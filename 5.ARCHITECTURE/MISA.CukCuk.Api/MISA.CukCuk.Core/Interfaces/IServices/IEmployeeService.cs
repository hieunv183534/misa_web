using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.IServices
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Lấy danh sách nhân viên theo các tiêu chí và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="positionId"></param>
        /// <param name="departmentId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        ServiceResult GetFilter(int pageSize, int pageNumber, Guid? positionId,Guid? departmentId , string searchTerms);
    }
}
