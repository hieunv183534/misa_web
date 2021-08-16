using Dapper;
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

        public IDbConnection dbConnection;
        string _tableName;

        public BaseRepository()
        {
            dbConnection = DatabaseConnection.DbConnection;
            _tableName = typeof(MISAEntity).Name;
        }

        /// <summary>-------------------------------------------------------------------------------
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(MISAEntity entity)
        {
            //// thêm id của employee là new guid
            //entity.CustomerId = Guid.NewGuid();

            /// complete sql String
            var colNames = string.Empty;
            var colParams = string.Empty;
            // đọc từng property
            var properties = entity.GetType().GetProperties();

            DynamicParameters parameters = new DynamicParameters();
            // duyệt từng property

            foreach (var prop in properties)
            {
                // lấy tên prop
                var propName = prop.Name;

                // lấy value prop
                var propValue = prop.GetValue(entity);
                //// lấy kiểu prop
                //var propType = prop.PropertyType;

                // nếu là id entity thì new Guid
                if(propName == $"{_tableName}Id")
                {
                    propValue = Guid.NewGuid().ToString();
                }


                parameters.Add($"@{propName}", propValue);
                colNames += $"{propName},";
                colParams += $"@{propName},";
            }
            colNames = colNames.Remove(colNames.Length - 1, 1);
            colParams = colParams.Remove(colParams.Length - 1, 1);
            var sql = $"insert into {_tableName}({colNames}) values( {colParams} ) ";
            var rowAffects = dbConnection.Execute(sql, param: parameters);
            return rowAffects;
        }

        /// <summary>-------------------------------------------------------------------------------
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public int Delete(Guid entityId)
        {
            var sql = $"delete from {_tableName} where {_tableName}Id = '{entityId}'";
            var rowAffects = dbConnection.Execute(sql);
            return rowAffects;
        }

        /// <summary>------------------------------------------------------------------------------
        /// Lấy tất cả bản ghi hiện có
        /// </summary>
        /// <returns></returns>
        public List<MISAEntity> GetAll()
        {
            var sql = $"select * from {_tableName}";
            var entities = dbConnection.Query<MISAEntity>(sql);
            return (List<MISAEntity>)entities;
        }

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
            var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sql, param: parameters);
            return entity;
        }

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
                // lấy tên prop
                var propName = prop.Name;
                // lấy value prop
                var propValue = prop.GetValue(entity);
                // lấy kiểu prop
                var propType = prop.PropertyType;

                parameters.Add($"@{propName}", propValue);
                cols += $" { propName } = @{propName},";
            }
            cols = cols.Remove(cols.Length - 1, 1);
            var sql = $"update {_tableName} set {cols} where {_tableName}Id = '{entityId}'";
            var rowAffects = dbConnection.Execute(sql, param: parameters);
            return rowAffects;
        }
    }
}
