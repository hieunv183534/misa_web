using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.IServices
{
    public interface ICustomerService
    {


        /// <summary>
        /// Lấy toàn bộ ds khách hàng
        /// </summary>
        /// <returns>ServiceResult</returns>
        /// Author hieunv 14/08/2021
        ServiceResult Get();

        /// <summary>
        /// Lấy thông tin khách hàng theo id
        /// </summary>
        /// <param name="customerIdStr">id của khách hàng cần get</param>
        /// <returns>ServiceResult</returns>
        /// Author hieunv 14/08/2021
        ServiceResult GetById(string customerIdStr);

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns>ServiceResult</returns>
        /// Author hieunv 14/08/2021
        ServiceResult Post(Customer customer);

        /// <summary>
        /// cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <param name="customerIdStr">id khách hàng câp nhật</param>
        /// <returns>ServiceResult</returns>
        /// Author hieunv14/08/2021
        ServiceResult Put(Customer customer, string customerIdStr);

        /// <summary>
        /// Xóa một khách hàng theo id
        /// </summary>
        /// <param name="customerIdStr">id khách hàng cần xóa</param>
        /// <returns>ServiceResult</returns>
        /// Author hieunv 14/08/2021
        ServiceResult Delete(string customerIdStr);


    }
}
