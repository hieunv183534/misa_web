using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.IRepository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// lấy dữ liệu phân trang và lọc theo tiêu chí
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="positionId"></param>
        /// <param name="departmentId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        PagingResult<Employee> GetFilter(int pageSize, int pageNumber, Guid? positionId, Guid? departmentId , string searchTerms);

        /// <summary>
        /// lấy dữ liệu theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        Employee GetByEmployeeCode(string employeeCode);
    }
}
