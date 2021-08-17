using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.IRepository;
using MISA.CukCuk.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository, IBaseRepository<Customer> baseRepository) :base(baseRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Xử lí nghiệp vụ cho chức năng tìm kiếm theo tiêu chí và phân trang cho ds khách hàng
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="customerGroupId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public ServiceResult GetFilter(int pageSize, int pageNumber, Guid? customerGroupId, string searchTerms)
        {
            try
            {
                //Xử lí nghiệp vụ
                //Lấy dữ liệu từ db
                PagingResult<Customer> pagingResult = _customerRepository.GetFilter(pageSize, pageNumber, customerGroupId, searchTerms);
                if (pagingResult.Data.Count() > 0)
                {
                    _serviceResult.Data = pagingResult;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.MISA_Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        ///// <summary>
        ///// Ghi đè Add của base để thêm chức năng validate
        ///// </summary>
        ///// <param name="customer"></param>
        ///// <returns></returns>
        //public override ServiceResult Add(Customer customer)
        //{
        //    // validate dữ liệu

        //    // kiểm tra email
        //    if (!Common.IsValidEmail(customer.Email))
        //    {
        //        var errorObj = new
        //        {
        //            devMsg = Resources.ResourceVN.MISA_Error_Dev_InvalidField,
        //            userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField
        //        };

        //        _serviceResult.Data = errorObj;
        //        _serviceResult.StatusCode = 400;
        //        return _serviceResult;
        //    }

        //    // kiểm tra mã khách hàng có null hoặc rỗng
        //    if (customer.CustomerCode == "" || customer.CustomerCode == null)
        //    {
        //        var errorObj = new
        //        {
        //            devMsg = Resources.ResourceVN.MISA_Error_Dev_NullField,
        //            userMsg = Resources.ResourceVN.MISA_Error_User_NullField
        //        };
        //        _serviceResult.Data = errorObj;
        //        _serviceResult.StatusCode = 400;
        //        return _serviceResult;
        //    }

        //    // kiểm tra mã khách hàng có bị trùng không
        //    var customerByCode = _customerRepository.GetByCustomerCode(customer.CustomerCode);
        //    if(customerByCode != null)
        //    {
        //        var errorObj = new
        //        {
        //            devMsg = Resources.ResourceVN.MISA_Error_Dev_DuplicateFiled,
        //            userMsg = Resources.ResourceVN.MISA_Error_User_DuplicateField
        //        };
        //        _serviceResult.Data = errorObj;
        //        _serviceResult.StatusCode = 400;
        //        return _serviceResult;
        //    }

        //    return base.Add(customer);
        //}

        ///// <summary>
        ///// Ghi đè update của base để thực hiện chức năng validate
        ///// </summary>
        ///// <param name="customer"></param>
        ///// <param name="customerId"></param>
        ///// <returns></returns>
        //public override ServiceResult Update(Customer customer, Guid customerId)
        //{
        //    //validate dữ liệu

        //    // kiểm tra email
        //    if (!Common.IsValidEmail(customer.Email))
        //    {
        //        var errorObj = new
        //        {
        //            devMsg = Resources.ResourceVN.MISA_Error_Dev_InvalidField,
        //            userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField
        //        };

        //        _serviceResult.Data = errorObj;
        //        _serviceResult.StatusCode = 400;
        //        return _serviceResult;
        //    }

        //    // kiểm tra mã khách hàng có null hoặc rỗng
        //    if (customer.CustomerCode == "" || customer.CustomerCode == null)
        //    {
        //        var errorObj = new
        //        {
        //            devMsg = Resources.ResourceVN.MISA_Error_Dev_NullField,
        //            userMsg = Resources.ResourceVN.MISA_Error_User_NullField
        //        };
        //        _serviceResult.Data = errorObj;
        //        _serviceResult.StatusCode = 400;
        //        return _serviceResult;
        //    }

        //    // kiểm tra mã khách hàng có bị trùng không
        //    var customerByCode = _customerRepository.GetByCustomerCode(customer.CustomerCode);
        //    if (customerByCode != null)
        //    {
        //        var errorObj = new
        //        {
        //            devMsg = Resources.ResourceVN.MISA_Error_Dev_DuplicateFiled,
        //            userMsg = Resources.ResourceVN.MISA_Error_User_DuplicateField
        //        };
        //        _serviceResult.Data = errorObj;
        //        _serviceResult.StatusCode = 400;
        //        return _serviceResult;
        //    }

        //    return base.Update(customer, customerId);
        //}
    }
}
