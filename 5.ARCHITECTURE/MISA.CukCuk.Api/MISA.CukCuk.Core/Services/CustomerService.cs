using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.IRepository;
using MISA.CukCuk.Core.Interfaces.IServices;
using OfficeOpenXml;
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
        #region Declare

        ICustomerRepository _customerRepository;
        static List<Customer> importAllowCustomers;
        IBaseRepository<CustomerGroup> _customerGroupRepo;

        #endregion

        #region Constructor

        public CustomerService(IBaseRepository<CustomerGroup> customerGroupRepo, ICustomerRepository customerRepository, IBaseRepository<Customer> baseRepository) : base(baseRepository)
        {
            _customerRepository = customerRepository;
            _customerGroupRepo = customerGroupRepo;
        }

        #endregion

        #region GetFilter

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

        #endregion

        #region Add

        /// <summary>
        /// Xử lí nghiệp vụ riêng cho customer trước khi truyền lên base
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ServiceResult Add(Customer entity)
        {
            // validate riêng cho customer
            // validate mã khách hàng dạng KH1234

            var customerCode = entity.GetType().GetProperty("CustomerCode").GetValue(entity);
            if (customerCode != null && customerCode.ToString() != "")
            {
                if (!Regex.IsMatch((string)customerCode, @"^((KH)(\d+))$"))
                {
                    _serviceResult.Data = new
                    {
                        devMsg = Resources.ResourceVN.MISA_Error_Dev_InvalidField,
                        userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField,
                    };
                    _serviceResult.StatusCode = 400;
                    return _serviceResult;
                }
            }

            // sử dụng tiếp hàm của base
            return base.Add(entity);
        }

        #endregion

        #region Update

        /// <summary>
        /// Xử lí nghiệp vụ riêng cho customer trước khi truyền lên base
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override ServiceResult Update(Customer entity, Guid entityId)
        {
            // validate riêng cho customer
            // validate mã khách hàng dạng KH1234

            var customerCode = entity.GetType().GetProperty("CustomerCode").GetValue(entity);
            if (customerCode != null && customerCode.ToString() != "")
            {
                if (!Regex.IsMatch((string)customerCode, @"^((KH)(\d+))$"))
                {
                    _serviceResult.Data = new
                    {
                        devMsg = Resources.ResourceVN.MISA_Error_Dev_InvalidField,
                        userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField,
                    };
                    _serviceResult.StatusCode = 400;
                    return _serviceResult;
                }
            }
            // sử dụng tiếp hàm của base
            return base.Update(entity, entityId);
        }
        #endregion

        #region GetAll

        public override ServiceResult GetAll()
        {
            try
            {
                // xử lí nghiệp vụ lấy dữ liệu
                // lấy tất cả dữ liệu từ db
                var entities = _customerRepository.GetAll();
                if (entities.Count() > 0)
                {
                    _serviceResult.Data = entities;
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

        #endregion

        #region Import

        /// <summary>
        /// Tiền xử lí dữ liệu import
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public ServiceResult Import(ExcelPackage package)
        {
            importAllowCustomers = new List<Customer>();
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;
            var customers = new List<Customer>();
            var listCode = new List<string>();
            var listPhoneNumber = new List<string>();
            List<CustomerGroup> customerGroups = _customerGroupRepo.GetAll();
            for (int row = 3; row <= rowCount; row++)
            {
                var customer = new Customer();
                customer.ErrorList = new List<string>();
                // validate CustomerGroup
                customer.CustomerGroupName = worksheet.Cells[row, 4].Value == null ? null : worksheet.Cells[row, 4].Value.ToString().Trim();
                if(customer.CustomerGroupName != null)
                {
                    int flag = 0;
                    foreach (var customerGroup in customerGroups)
                    {
                        if (customerGroup.CustomerGroupName == customer.CustomerGroupName)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                        customer.ErrorList.Add("Nhóm khách hàng không có trong hệ thống.");
                }
      
                // validate mã khách hàng
                customer.CustomerCode = worksheet.Cells[row, 1].Value == null ? null :worksheet.Cells[row, 1].Value.ToString().Trim();
                if(customer.CustomerCode != null)
                {
                    if (listCode.Contains(customer.CustomerCode))
                    {
                        customer.ErrorList.Add("Mã khách hàng đã trùng với khách hàng khác trong tệp nhập khẩu.");
                    }
                    if(_baseRepository.GetByProp("CustomerCode", customer.CustomerCode) != null)
                    {
                        customer.ErrorList.Add("Mã khách hàng đã tồn tại trong hệ thống.");
                    }
                    listCode.Add(customer.CustomerCode);
                }

                //validate số điện thoại
                customer.PhoneNumber = worksheet.Cells[row, 5].Value == null ?
                    null : worksheet.Cells[row, 5].Value.ToString().Replace(".","").Replace(" ","");
                if (customer.PhoneNumber != null)
                {
                    if (listPhoneNumber.Contains(customer.PhoneNumber))
                    {
                        customer.ErrorList.Add(" SĐT đã trùng với SĐT của khách hàng khác trong tệp nhập khẩu.");
                    }
                    if (_baseRepository.GetByProp("PhoneNumber", customer.PhoneNumber) != null)
                    {
                        customer.ErrorList.Add("SĐT đã có trong hệ thống.");
                    }
                    listPhoneNumber.Add(customer.PhoneNumber);
                }

                // Validate ngày sinh
                var dateOfBirth = worksheet.Cells[row, 6].Value == null ? null : worksheet.Cells[row, 6].Value.ToString().Trim();
                if(dateOfBirth!= null)
                {
                    if(Regex.IsMatch((string)dateOfBirth, @"^(\d+)$"))
                    {
                        try
                        {
                            var year = Int32.Parse(dateOfBirth);
                            customer.DateOfBirth = new DateTime(year, 1,1);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }else if(Regex.IsMatch((string)dateOfBirth, @"^((\d+)/(\d+))$"))
                    {
                        string[] my = dateOfBirth.Split("/");
                        try
                        {
                            var year = Int32.Parse(my[1]);
                            var month = Int32.Parse(my[0]);
                            customer.DateOfBirth = new DateTime(year, month, 1);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else if(Regex.IsMatch((string)dateOfBirth, @"^((\d+)/(\d+)/(\d+))$"))
                    {
                        string[] dmy = dateOfBirth.Split("/");
                        try
                        {
                            var year = Int32.Parse(dmy[2]);
                            var month = Int32.Parse(dmy[1]);
                            var day = Int32.Parse(dmy[0]);
                            customer.DateOfBirth = new DateTime(year, month, day);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                // Gán các prop còn lại
                customer.FullName = worksheet.Cells[row, 2].Value == null ? null : worksheet.Cells[row, 2].Value.ToString().Trim();
                customer.MemberCardCode = worksheet.Cells[row, 3].Value == null ? null : worksheet.Cells[row, 3].Value.ToString().Trim();
                customer.CompanyName = worksheet.Cells[row, 7].Value == null ? null : worksheet.Cells[row, 7].Value.ToString().Trim();
                customer.Email = worksheet.Cells[row, 9].Value == null ? null : worksheet.Cells[row, 9].Value.ToString().Trim();
                customer.Address = worksheet.Cells[row, 10].Value == null ? null : worksheet.Cells[row, 10].Value.ToString().Trim();

                customers.Add(customer);
                if (customer.ErrorList.Count() == 0)
                    importAllowCustomers.Add(customer);
            }
            _serviceResult.Data = customers;
            _serviceResult.StatusCode = 200;
            return _serviceResult;
        }


        #endregion
    }
}
