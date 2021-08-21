using Dapper;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infrastructure.Repositiory
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        #region Declare

        string _tableName;

        #endregion

        #region Consrtuctor

        public BaseRepository()
        {
            _tableName = typeof(MISAEntity).Name;
        }

        #endregion

        #region Add

        /// <summary>-------------------------------------------------------------------------------
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(MISAEntity entity)
        {
            var sql = RenderSqlStrForAdd(entity).SqlString;
            var parameters = RenderSqlStrForAdd(entity).Parameters;
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var rowAffects = dbConnection.Execute(sql, param: parameters);
                return rowAffects;
            }
        }
        #endregion

        #region AddMany

        public int AddMany(List<MISAEntity> entities)
        {
            var dbConnection = DatabaseConnection.DbConnection;
            var transction = dbConnection.BeginTransaction();
            var rowAffects = 0;
            foreach (var entity in entities)
            {
                var sql = RenderSqlStrForAdd(entity).SqlString;
                var parameters = RenderSqlStrForAdd(entity).Parameters;
                rowAffects += dbConnection.Execute(sql, param: parameters);
            }
            transction.Commit();
            if (rowAffects < entities.Count())
                rowAffects = 0;
            return rowAffects;
        }

        #endregion

        #region Delete

        /// <summary>-------------------------------------------------------------------------------
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public int Delete(Guid entityId)
        {
            var sql = $"delete from {_tableName} where {_tableName}Id = '{entityId}'";
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var rowAffects = dbConnection.Execute(sql);
                return rowAffects;
            }
        }

        #endregion

        #region GetAll

        /// <summary>------------------------------------------------------------------------------
        /// Lấy tất cả bản ghi hiện có
        /// </summary>
        /// <returns></returns>
        public virtual List<MISAEntity> GetAll()
        {
            var sql = $"select * from {_tableName}";
            using(var dbConnection = DatabaseConnection.DbConnection)
            {
                var entities = dbConnection.Query<MISAEntity>(sql);
                return (List<MISAEntity>)entities;
            } 
        }

        #endregion

        #region GetById

        /// <summary>---------------------------------------------------------------------------
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public MISAEntity GetById(Guid entityId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", entityId);
            var sql = $"select * from {_tableName} where {_tableName}Id= @{_tableName}Id";
            using(var dbConnection = DatabaseConnection.DbConnection)
            {
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sql, param: parameters);
                return entity;
            }     
        }

        #endregion

        #region GetByProp

        /// <summary>
        /// Lấy 1 bản ghi theo 1 prop nào đó
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public MISAEntity GetByProp(string propName, object propValue)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{propName}", propValue.ToString());
            var sql = $"select * from {_tableName} where {propName}= @{propName} ";
            using(var dbConnection = DatabaseConnection.DbConnection)
            {
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sql, param: parameters);
                return entity;
            }        
        }

        #endregion

        #region Update

        /// <summary>----------------------------------------------------------------------------
        /// cập nhật một bản ghi với id
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public int Update(MISAEntity entity, Guid entityId)
        {
            /// complete sql String
            var cols = string.Empty;
            // đọc từng property
            var properties = entity.GetType().GetProperties();
            DynamicParameters parameters = new DynamicParameters();
            // duyệt từng property

            foreach (var prop in properties)
            {
                if(!prop.IsDefined(typeof(NotMap), false))
                {
                    // lấy tên prop
                    var propName = prop.Name;
                    // lấy value prop
                    var propValue = prop.GetValue(entity);
                    // lấy kiểu prop
                    var propType = prop.PropertyType;

                    parameters.Add($"@{propName}", propValue);
                    cols += $" { propName } = @{propName},";
                }
            }
            cols = cols.Remove(cols.Length - 1, 1);
            var sql = $"update {_tableName} set {cols} where {_tableName}Id = '{entityId}'";
            using(var dbConnection = DatabaseConnection.DbConnection)
            {
                var rowAffects = dbConnection.Execute(sql, param: parameters);
                return rowAffects;
            }        
        }

        #endregion

        #region RenderSqlStrForAdd

        /// <summary>
        /// tạo sql và parameter cho insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private SqlResult RenderSqlStrForAdd(MISAEntity entity)
        {
            var sqlResult = new SqlResult();
            /// complete sql String
            var colNames = string.Empty;
            var colParams = string.Empty;
            // đọc từng property
            var properties = entity.GetType().GetProperties();

            DynamicParameters parameters = new DynamicParameters();
            // duyệt từng property

            foreach (var prop in properties)
            {
                if (!prop.IsDefined(typeof(NotMap), false))
                {
                    // lấy tên prop
                    var propName = prop.Name;

                    // lấy value prop
                    var propValue = prop.GetValue(entity);
                    //// lấy kiểu prop
                    //var propType = prop.PropertyType;

                    // nếu là id entity thì new Guid
                    if (propName == $"{_tableName}Id")
                    {
                        propValue = Guid.NewGuid().ToString();
                    }
                    parameters.Add($"@{propName}", propValue);
                    colNames += $"{propName},";
                    colParams += $"@{propName},";
                }
            }
            colNames = colNames.Remove(colNames.Length - 1, 1);
            colParams = colParams.Remove(colParams.Length - 1, 1);
            var sql = $"insert into {_tableName}({colNames}) values( {colParams} ) ";
            sqlResult.SqlString = sql;
            sqlResult.Parameters = parameters;
            return sqlResult;
        }

        #endregion

    }

    class SqlResult
    {
        public string SqlString { get; set; }
        public DynamicParameters Parameters { get; set; }
    }
}
