using Dapper;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infrastructure.Repositiory
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region GetFilter

        /// <summary>
        /// Lọc nhâna viên phân trang theo các tiêu chí
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="positionId"></param>
        /// <param name="departmentId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public PagingResult<Employee> GetFilter(int pageSize, int pageNumber, Guid? positionId, Guid? departmentId, string searchTerms)
        {
            DynamicParameters parameters = new DynamicParameters();
            var sql = $"select * from Employee ";
            var sqlCondition = $"where 1=1 ";

            // nếu có search thì thêm điều kiệu sql là hoặc fullname, hoặc mã khách hàng, hoặc số điện thoại
            if (searchTerms != null && searchTerms != "")
            {
                parameters.Add($"@FullName", searchTerms);
                parameters.Add($"@EmployeeCode", searchTerms);
                parameters.Add($"@PhoneNumber", searchTerms);
                sqlCondition += $" and ( FullName = @FullName or EmployeeCode=@EmployeeCode or PhoneNumber=@PhoneNumber ) ";
            }

            // nếu PositionId được nhận và là 1 guid thì thêm điều kiện and PositionId = @PositionId
            if (positionId != null)
            {
                parameters.Add($"@PositionId", positionId);
                sqlCondition += $" and PositionId = @PositionId ";
            }

            // nếu DepartmentId được nhận và là 1 guid thì thêm điều kiện and DepartmentId = @DepartmentId
            if (positionId != null)
            {
                parameters.Add($"@DepartmentId", departmentId);
                sqlCondition += $" and DepartmentId = @DepartmentId ";
            }
            

            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                // lấy tổng số bản ghi 
                var sqlCount = $"select count(EmployeeId) as TotalRecord from Employee " + sqlCondition;
                int TotalRecord = (int)dbConnection.QueryFirstOrDefault(sqlCount, param: parameters).TotalRecord;

                // Xử lsi điều kiện limit, offset
                var limit = pageSize;
                var offset = (pageNumber - 1) * pageSize;
                parameters.Add($"@limit", limit);
                parameters.Add($"@offset", offset);
                sql += sqlCondition;
                sql += $" limit @offset, @limit ";

                // thực hiện truy vấn
                var employees = dbConnection.Query<Employee>(sql, param: parameters);

                return new PagingResult<Employee>(TotalRecord, (List<Employee>)employees);
            }
        }

        #endregion   
    }
}
