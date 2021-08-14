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
    public class CustomerService : ICustomerService
    {
        ICustomerRepository _customerRepository;
        ServiceResult _serviceResult;


        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public CustomerService()
        {
            _serviceResult = new ServiceResult();
        }

        public ServiceResult Get()
        {
            // xử lí nghiệp vụ

            //tương tác với database
            _serviceResult.Data = _customerRepository.Get();
            if (_customerRepository.Get().Count() > 0)
            {
                _serviceResult.StatusCode = 200;
            }
            else
            {
                _serviceResult.StatusCode = 204;
            }

            return _serviceResult;
        }

        public ServiceResult Delete(string customerIdStr)
        {
            // xử lí nghiệp vụ

            //tương tác với database

            throw new NotImplementedException();
        }

        public ServiceResult GetById(string customerIdStr)
        {
            // xử lí nghiệp vụ

            //tương tác với database

            throw new NotImplementedException();
        }

        public ServiceResult Post(Customer customer)
        {
            /// xử lí nghiệp vụ

            // Kiểm tra mã khách hàng
            if (customer.CustomerCode == "" || customer.CustomerCode == null)
            {
                var errorObj = new
                {
                    devMsg = Resources.ResourceVN.MISA_Error_Dev_NullField,
                    userMsg = Resources.ResourceVN.MISA_Error_User_NullField
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 400;
                return _serviceResult;
            }

            // kiểm tra email
            if (
                !Regex.IsMatch(customer.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                )
            {
                var errorObj = new
                {
                    devMsg =  Resources.ResourceVN.MISA_Error_Dev_InvalidField,
                    userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 400;
                return _serviceResult;
            }

            // kiểm tra mã khách hàng có bị trùng không

            ///tương tác với database qua ICustomerRepository
            _serviceResult.Data = _customerRepository.Post(customer);
            if((int)_serviceResult.Data == 1)
            {
                _serviceResult.StatusCode = 201;
            }
            else
            {
                _serviceResult.StatusCode = 204;
            }
            _serviceResult.IsValid = true;

            return _serviceResult;
        }

        public ServiceResult Put(Customer customer, string customerIdStr)
        {
            // xử lí nghiệp vụ

            //tương tác với database

            throw new NotImplementedException();
        }
    }
}
