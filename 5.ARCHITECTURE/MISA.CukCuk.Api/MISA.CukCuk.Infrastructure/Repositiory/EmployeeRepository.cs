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
            var sql = $" SELECT e.EmployeeId AS EmployeeId, e.EmployeeCode AS EmployeeCode, e.FirstName AS FirstName, e.LastName AS LastName, e.FullName AS FullName, " +
                " e.Gender AS Gender, e.DateOfBirth AS DateOfBirth, e.PhoneNumber AS PhoneNumber, e.Email AS Email, e.Address AS Address, " +
                " e.IdentityNumber AS IdentityNumber, e.IdentityDate AS IdentityDate,e.IdentityPlace AS IdentityPlace, e.JoinDate AS JoinDate, " +
                " e.MartialStatus AS MartialStatus, e.PersonalTaxCode AS PersonalTaxCode, e.Salary AS Salary, e.EducationalBackground AS EducationalBackground, " +
                " e.WorkStatus AS WorkStatus, p.PositionId AS PositionId, p.PositionCode AS PositionCode, p.PositionName AS PositionName, d.DepartmentId AS DepartmentId, " +
                " d.DepartmentCode AS DepartmentCode, d.DepartmentName AS DepartmentName, e.CreatedDate AS CreatedDate, e.CreatedBy AS CreatedBy, " +
                " e.ModifiedDate AS ModifiedDate,e.ModifiedBy AS ModifiedBy " +
                " FROM (((Employee e " +
                "   LEFT JOIN `Position` p " +
                " ON ((e.PositionId = p.PositionId))) " +
                " LEFT JOIN Department d " +
                " ON ((e.DepartmentId = d.DepartmentId)))) ";
            var sqlCondition = $"where 1=1 ";

            // nếu có search thì thêm điều kiệu sql là hoặc fullname, hoặc mã khách hàng, hoặc số điện thoại
            if (searchTerms != null && searchTerms != "")
            {
                parameters.Add($"@EmployeeCode", searchTerms);
                parameters.Add($"@PhoneNumber", searchTerms);
                sqlCondition += $" and ( FullName like '%{searchTerms}%' or EmployeeCode=@EmployeeCode or PhoneNumber=@PhoneNumber ) ";
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

        #region GetAllEmployee

        public override List<Employee> GetAll()
        {
            var sql = $" SELECT e.EmployeeId AS EmployeeId, e.EmployeeCode AS EmployeeCode, e.FirstName AS FirstName, e.LastName AS LastName, e.FullName AS FullName, " +
                " e.Gender AS Gender, e.DateOfBirth AS DateOfBirth, e.PhoneNumber AS PhoneNumber, e.Email AS Email, e.Address AS Address, " +
                " e.IdentityNumber AS IdentityNumber, e.IdentityDate AS IdentityDate,e.IdentityPlace AS IdentityPlace, e.JoinDate AS JoinDate, " +
                " e.MartialStatus AS MartialStatus, e.PersonalTaxCode AS PersonalTaxCode, e.Salary AS Salary, e.EducationalBackground AS EducationalBackground, " +
                " e.WorkStatus AS WorkStatus, p.PositionId AS PositionId, p.PositionCode AS PositionCode, p.PositionName AS PositionName, d.DepartmentId AS DepartmentId, " +
                " d.DepartmentCode AS DepartmentCode, d.DepartmentName AS DepartmentName, e.CreatedDate AS CreatedDate, e.CreatedBy AS CreatedBy, " +
                " e.ModifiedDate AS ModifiedDate,e.ModifiedBy AS ModifiedBy " +
                " FROM (((Employee e " +
                "   LEFT JOIN `Position` p " +
                " ON ((e.PositionId = p.PositionId))) " +
                " LEFT JOIN Department d " +
                " ON ((e.DepartmentId = d.DepartmentId)))) ";
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var employees = dbConnection.Query<Employee>(sql);
                return (List<Employee>)employees;
            }
        }

        #endregion
    }
}
