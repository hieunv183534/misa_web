using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.IServices
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Lấy danh sách dữ liệu phân trang và heo một số tiêu chí
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="customerGroupId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        ServiceResult GetFilter(int pageSize, int pageNumber, Guid? customerGroupId, string searchTerms);
    }
}
