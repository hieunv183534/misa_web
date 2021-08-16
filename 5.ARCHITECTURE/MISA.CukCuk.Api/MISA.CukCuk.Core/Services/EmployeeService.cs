﻿using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.IRepository;
using MISA.CukCuk.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ServiceResult GetFilter(int pageSize, int pageNumber, Guid? positionId, Guid? departmentId, string searchTerms)
        {
            try
            {
                //Xử lí nghiệp vụ
                //Lấy dữ liệu từ db
                PagingResult<Employee> pagingResult = _employeeRepository.GetFilter(pageSize, pageNumber, positionId, departmentId, searchTerms);
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

        public override ServiceResult Add(Employee employee)
        {
            // validate dữ liệu

            // kiểm tra email
            if (!Common.IsValidEmail(employee.Email))
            {
                var errorObj = new
                {
                    devMsg = Resources.ResourceVN.MISA_Error_Dev_InvalidField,
                    userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField
                };

                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 400;
                return _serviceResult;
            }

            // kiểm tra mã nhân viên có null hoặc rỗng
            if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
            {
                var errorObj = new
                {
                    devMsg = Resources.ResourceVN.MISA_Error_Dev_NullField,
                    userMsg = Resources.ResourceVN.MISA_Error_User_NullField
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 400;
                return _serviceResult;
            }

            // kiểm tra mã nhân viên có bị trùng không
            var employeeByCode = _employeeRepository.GetByEmployeeCode(employee.EmployeeCode);
            if (employeeByCode != null)
            {
                var errorObj = new
                {
                    devMsg = Resources.ResourceVN.MISA_Error_Dev_DuplicateFiled,
                    userMsg = Resources.ResourceVN.MISA_Error_User_DuplicateField
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 400;
                return _serviceResult;
            }

            return base.Add(employee);
        }

        public override ServiceResult Update(Employee employee, Guid employeeId)
        {
            // validate dữ liệu

            // kiểm tra email
            if (!Common.IsValidEmail(employee.Email))
            {
                var errorObj = new
                {
                    devMsg = Resources.ResourceVN.MISA_Error_Dev_InvalidField,
                    userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField
                };

                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 400;
                return _serviceResult;
            }

            // kiểm tra mã nhân viên có null hoặc rỗng
            if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
            {
                var errorObj = new
                {
                    devMsg = Resources.ResourceVN.MISA_Error_Dev_NullField,
                    userMsg = Resources.ResourceVN.MISA_Error_User_NullField
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 400;
                return _serviceResult;
            }

            // kiểm tra mã nhân viên có bị trùng không
            var employeeByCode = _employeeRepository.GetByEmployeeCode(employee.EmployeeCode);
            if (employeeByCode != null)
            {
                var errorObj = new
                {
                    devMsg = Resources.ResourceVN.MISA_Error_Dev_DuplicateFiled,
                    userMsg = Resources.ResourceVN.MISA_Error_User_DuplicateField
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 400;
                return _serviceResult;
            }

            return base.Update(employee, employeeId);
        }
    }
}
