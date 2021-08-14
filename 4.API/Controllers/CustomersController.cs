using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.api.Controllers
{
    /// <summary>
    /// api danh mục khách hàng
    /// Author hieunv 12/08/2021
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        IDbConnection dbConnection = DatabaseConnection.DbConnection;

        #region Lấy toàn bộ ds khách hàng

        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// Author hieunv 12/08/2021
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var sql = "select * from Customer";
                var customers = dbConnection.Query<Customer>(sql);
                if (customers.Count() > 0)
                {
                    return StatusCode(200, customers);
                }
                else
                {
                    return StatusCode(204, customers);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.ResourceVN.MISA_Error,
                };
                return StatusCode(500, errorObj);

            }
        }

        #endregion

        #region Lấy thông tin 1 khách hàng theo id

        /// <summary>
        /// Lấy thông tin khách hàng theo id
        /// </summary>
        /// <param name="CustomerIdStr"></param>
        /// <returns>Khách hàng</returns>
        /// Author hieunv 03/08/2021
        [HttpGet("{CustomerIdStr}")]
        public IActionResult GetById(string CustomerIdStr)
        {
            Guid CustomerId;
            try
            {
                // kiểm tra xem id truyền lên có đúng là guid hay chưa
                try
                {
                    CustomerId = Guid.Parse(CustomerIdStr);
                }
                catch (Exception ex)
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.ResourceVN.MISA_Error,
                    };
                    return StatusCode(400, errorObj);
                }
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", CustomerId);
                var sql = $"select * from Customer where CustomerId= @CustomerId";
                var customer = dbConnection.QueryFirstOrDefault(sql, param: parameters);
                return StatusCode(200, customer);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.ResourceVN.MISA_Error
                };
                return StatusCode(500, errorObj);
            }
        }

        #endregion

        #region Lấy thông tin ds khách hàng theo những tiêu chí
        /// <summary>
        /// Lấy ds khách hàng theo các tiêu chí và phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi / trang</param>
        /// <param name="pageNumber">chỉ số trang</param>
        /// <param name="CustomerGroupIdSttr">id vị trí dang string</param>
        /// <param name="searchTerms"></param>
        /// <returns>trả về status code và dữ liệu tương ứng các th xảy ra</returns>
        [HttpGet("Fitler")]
        public IActionResult GetFitler([FromQuery] int pageSize, [FromQuery] int pageNumber,
            [FromQuery] string CustomerGroupIdSttr, [FromQuery] string searchTerms)
        {

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                var sql = $"select * from Customer ";
                var sqlCondition = $"where 1=1 ";
                Guid? CustomerGroupId = null;
                // kiểm tra xem CustomerGroupId truyền lên có đúng là guid hay chưa, nếu null thì tức ko nhận từ query-> bỏ qua
                try
                {
                    if (CustomerGroupIdSttr != null)
                    {
                        CustomerGroupId = Guid.Parse(CustomerGroupIdSttr);
                    }
                }
                catch (Exception ex)
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.ResourceVN.MISA_Error,
                    };
                    return StatusCode(400, errorObj);
                }
                
                // nếu có search thì thêm điều kiệu sql là hoặc fullname, hoặc mã khách hàng, hoặc số điện thoại
                if(searchTerms != null && searchTerms !="")
                {
                    parameters.Add($"@FullName", searchTerms);
                    parameters.Add($"@CustomerCode", searchTerms);
                    parameters.Add($"@PhoneNumber", searchTerms);
                    sqlCondition += $" and ( FullName = @FullName or CustomerCode=@CustomerCode or PhoneNumber=@PhoneNumber ) ";
                }

                // nếu CustomerGroupId được nhận và là 1 guid thì thêm điều kiện and CustomerGroupId = @CustomerGroupId
                if (CustomerGroupId != null)
                {
                    parameters.Add($"@CustomerGroupId", CustomerGroupId);
                    sqlCondition += $" and CustomerGroupId = @CustomerGroupId ";
                }

                // lấy tổng số bản ghi 
                var sqlCount = $"select count(CustomerId) as TotalRecord from Customer " + sqlCondition;
                var TotalRecord = dbConnection.QueryFirstOrDefault(sqlCount, param: parameters).TotalRecord;

                // Xử lsi điều kiện limit, offset
                var limit = pageSize;
                var offset = (pageNumber - 1) * pageSize;
                parameters.Add($"@limit", limit);
                parameters.Add($"@offset", offset);
                sql += sqlCondition;
                sql += $" limit @offset, @limit ";

                // thực hiện truy vấn
                var customers = dbConnection.Query<Customer>(sql,param: parameters);

                if (customers.Count() > 0)
                {
                    var response = new
                    {
                        TotalRecord = TotalRecord,
                        Data = customers
                    };
                    return StatusCode(200, response);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.ResourceVN.MISA_Error,
                };
                return StatusCode(500, errorObj);
            }
            
        }

        #endregion

        #region Thêm mới một khách hàng

        /// <summary>
        /// Thêm mới một khách hàng vào database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>số hàng được thêm thành công</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                // Kiểm tra mã khách hàng

                if (customer.CustomerCode == "" || customer.CustomerCode == null)
                {
                    var errorObj = new
                    {
                        devMsg = Properties.ResourceVN.MISA_Error_Dev_NullField,
                        userMsg = Properties.ResourceVN.MISA_Error_User_NullField
                    };
                    return StatusCode(400, errorObj);
                }

                // kiểm tra email

                if (
                    !Regex.IsMatch(customer.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                    )
                {
                    var errorObj = new
                    {
                        devMsg = Properties.ResourceVN.MISA_Error_Dev_InvalidField,
                        userMsg = Properties.ResourceVN.MISA_Error_User_InvalidField
                    };
                    return StatusCode(400, errorObj);
                }

                // thêm id của employee là new guid
                customer.CustomerId = Guid.NewGuid();

                /// complete sql String
                var colNames = string.Empty;
                var colParams = string.Empty;
                // đọc từng property
                var properties = customer.GetType().GetProperties();

                DynamicParameters parameters = new DynamicParameters();
                // duyệt từng property

                foreach (var prop in properties)
                {
                    // lấy tên prop
                    var propName = prop.Name;
                    // lấy value prop
                    var propValue = prop.GetValue(customer);
                    // lấy kiểu prop
                    var propType = prop.PropertyType;

                    parameters.Add($"@{propName}", propValue);
                    colNames += $"{propName},";
                    colParams += $"@{propName},";
                }
                colNames = colNames.Remove(colNames.Length - 1, 1);
                colParams = colParams.Remove(colParams.Length - 1, 1);
                var sql = $"insert into Customer({colNames}) values( {colParams} ) ";
                var rowAffects = dbConnection.Execute(sql, param: parameters);
                var response = StatusCode(201, rowAffects);
                return response;
            }
            catch (Exception ex)
            {
                // lỗi trùng mã khách hàng (khi exception trả về chứa Duplicate)
                if (ex.Message.Contains("Duplicate"))
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.ResourceVN.MISA_Error_User_DuplicateField
                    };
                    return StatusCode(400, errorObj);
                }
                else
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.ResourceVN.MISA_Error
                    };
                    return StatusCode(500, errorObj);
                }
            }
        }

        #endregion

        #region Cập nhật một khách hàng có id được chọn

        /// <summary>
        /// Cập nhật một khách hàng với CustomerId trước
        /// </summary>
        /// <param name="CustomerIdStr"> lấy từ route</param>
        /// <param name="customer"> lấy từ body</param>
        /// <returns>1 nếu sửa thành công và 0 là ngược lại</returns>
        [HttpPut("{CustomerIdStr}")]
        public IActionResult Put([FromRoute] string CustomerIdStr, [FromBody] Customer customer)
        {

            try
            {
                Guid CustomerId;
                // kiểm tra xem id truyền lên có đúng là guid hay chưa
                try
                {
                    CustomerId = Guid.Parse(CustomerIdStr);
                }
                catch (Exception ex)
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.ResourceVN.MISA_Error,
                    };
                    return StatusCode(400, errorObj);
                }

                // Kiểm tra mã nhân viên
                if (customer.CustomerCode == "" || customer.CustomerCode == null)
                {
                    var errorObj = new
                    {
                        devMsg = Properties.ResourceVN.MISA_Error_Dev_NullField,
                        userMsg = Properties.ResourceVN.MISA_Error_User_NullField
                    };
                    return StatusCode(400, errorObj);
                }

                // kiểm tra email
                if (
                    !Regex.IsMatch(customer.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                    )
                {
                    var errorObj = new
                    {
                        devMsg = Properties.ResourceVN.MISA_Error_Dev_InvalidField,
                        userMsg = Properties.ResourceVN.MISA_Error_User_InvalidField
                    };
                    return StatusCode(400, errorObj);
                }


                /// complete sql String
                var cols = string.Empty;
                // đọc từng property
                var properties = customer.GetType().GetProperties();
                DynamicParameters parameters = new DynamicParameters();
                // duyệt từng property

                foreach (var prop in properties)
                {
                    // lấy tên prop
                    var propName = prop.Name;
                    // lấy value prop
                    var propValue = prop.GetValue(customer);
                    // lấy kiểu prop
                    var propType = prop.PropertyType;

                    parameters.Add($"@{propName}", propValue);
                    cols += $" { propName } = @{propName},";
                }
                cols = cols.Remove(cols.Length - 1, 1);
                var sql = $"update Customer set {cols} where CustomerId = {CustomerId}";
                var rowAffects = dbConnection.Execute(sql, param: parameters);
                var response = StatusCode(200, rowAffects);
                return response;
            }
            catch (Exception ex)
            {
                // lỗi trùng mã khách hàng (khi exception trả về chứa Duplicate)
                if (ex.Message.Contains("Duplicate"))
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.ResourceVN.MISA_Error_User_DuplicateField
                    };
                    return StatusCode(400, errorObj);
                }
                else
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.ResourceVN.MISA_Error
                    };
                    return StatusCode(500, errorObj);
                }
            }
        }

        #endregion

        #region Xóa một khách hàng theo id

        /// <summary>
        /// Xóa một khách hàng theo CustomerId
        /// </summary>
        /// <param name="CustomerIdStr"></param>
        /// <returns>1 nếu xóa thành công và 0 là ngược lại</returns>
        [HttpDelete("{CustomerIdStr}")]
        public IActionResult Delete(string CustomerIdStr)
        {

            try
            {
                Guid CustomerId;
                // kiểm tra xem id truyền lên có đúng là guid hay chưa
                try
                {
                    CustomerId = Guid.Parse(CustomerIdStr);
                }
                catch (Exception ex)
                {
                    var errorObj = new
                    {
                        devMsg = ex.Message,
                        userMsg = Properties.ResourceVN.MISA_Error,
                    };
                    return StatusCode(400, errorObj);
                }

                var sql = $"delete from Customer where CustomerId = {CustomerId}";
                var rowAffects = dbConnection.Execute(sql);
                var response = StatusCode(200, rowAffects);
                return response;
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.ResourceVN.MISA_Error
                };
                return StatusCode(500, errorObj);
            }
        }

        #endregion

    }
}
