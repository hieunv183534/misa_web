using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.IRepository
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Truy vấn toàn bộ khách hàng
        /// </summary>
        /// <returns></returns>
        List<Customer> Get();

        /// <summary>
        /// Truy vấn khách hàng theo id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Customer GetById(Guid customerId);

        /// <summary>
        /// Insert một khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        int Post(Customer customer);

        /// <summary>
        /// update một khách hàng theo id
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        int Put(Customer customer, Guid customerId);

        /// <summary>
        /// Xóa một khách hàng theo id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        int Delete( Guid customerId);
    }
}
