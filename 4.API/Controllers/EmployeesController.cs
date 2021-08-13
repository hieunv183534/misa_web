﻿using Dapper;
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
    /// api danh mục nhân viên
    /// Author hieunv 12/08/2021
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IDbConnection dbConnection = DatabaseConnection.DbConnection;

        #region Lấy toàn bộ ds nhân viên

        /// <summary>
        /// Lấy toàn bộ danh sách nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// Author hieunv 12/08/2021
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var sql = "select * from Employee";
                var employees = dbConnection.Query<Employee>(sql);
                if (employees.Count() > 0)
                {
                    return StatusCode(200, employees);
                }
                else
                {
                    return StatusCode(204, employees);
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

        #region Lấy thông tin nhân viên theo id

        /// <summary>
        /// Lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="EmployeeIdStr"></param>
        /// <returns>Nhân viên/returns>
        /// Author hieunv 12/08/2021
        [HttpGet("{EmployeeId}")]
        public IActionResult GetById(string EmployeeIdStr)
        {
            Guid EmployeeId;
            try
            {
                // kiểm tra xem id truyền lên có đúng là guid hay chưa
                try
                {
                    EmployeeId = Guid.Parse(EmployeeIdStr);
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
                parameters.Add("@EmployeeId", EmployeeId);
                var sql = $"select * from Employee where EmployeeId= @EmployeeId";
                var employee = dbConnection.QueryFirstOrDefault(sql, param: parameters);
                return StatusCode(200, employee);
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

        #region Lấy thông tin ds nhân viên theo những tiêu chí
        /// <summary>
        /// Lấy ds nhân viên theo các tiêu chí và phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi / trang</param>
        /// <param name="pageNumber">chỉ số trang</param>
        /// <param name="positionIdSttr">id vị trí dang string</param>
        /// <param name="departmentIdStr">id phòng ban dạng string</param>
        /// <param name="searchTerms"></param>
        /// <returns>trả về status code và dữ liệu tương ứng các th xảy ra</returns>
        [HttpGet("Fitler")]
        public IActionResult GetFitler([FromQuery] int pageSize, [FromQuery] int pageNumber,
            [FromQuery] string positionIdSttr, [FromQuery] string departmentIdStr, [FromQuery] string searchTerms)
        {

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                var sql = $"select * from Employee where ";
                Guid? PositionId = null;
                Guid? DepartmentId = null;
                // kiểm tra xem PositionId, DepartmentId truyền lên có đúng là guid hay chưa, nếu null thì tức ko nhận từ query-> bỏ qua
                try
                {
                    if (positionIdSttr != null)
                    {
                        PositionId = Guid.Parse(positionIdSttr);
                    }
                    if (departmentIdStr != null)
                    {
                        DepartmentId = Guid.Parse(departmentIdStr);
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

                // nếu có search thì thêm điều kiệu sql là hoặc fullname, hoặc mã nhân viên, hoặc số điện thoại
                if (searchTerms != null || searchTerms != "")
                {
                    parameters.Add($"@FullName", searchTerms);
                    parameters.Add($"@EmployeeCode", searchTerms);
                    parameters.Add($"@PhoneNumber", searchTerms);
                    sql += $"( FullName = @FullName or EmployeeCode=@EmployeeCode or PhoneNumber=@PhoneNumber ) ";
                }

                // nếu positionId được nhận và là 1 guid thì thêm điều kiện and positionId = @positionId
                if (PositionId != null)
                {
                    parameters.Add($"@PositionId", PositionId);
                    sql += $" and PositionId = @PositionId ";
                }
                // nếu departmentId được nhận và là 1 guid thì thêm điều kiện and departmentId = @departmentId
                if (DepartmentId != null)
                {
                    parameters.Add($"@DepartmentId", DepartmentId);
                    sql += $" and DepartmentId = @DepartmentId ";
                }

                // Xử lí điều kiện limit, offset
                var limit = pageSize;
                var offset = (pageNumber - 1) * pageSize;
                parameters.Add($"@limit", limit);
                parameters.Add($"@offset", offset);
                sql += $" limit = @limit offset = @offset ";

                // thực hiện truy vấn
                var employees = dbConnection.Query<Employee>(sql, param: parameters);
                if (employees.Count() > 0)
                {
                    return StatusCode(200, employees);
                }
                else
                {
                    return StatusCode(204, employees);
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

        #region Thêm mới một nhân viên

        /// <summary>
        /// Thêm mới một nhân viên vào database
        /// </summary>
        /// <param name="employee"></param>
        /// Author hieunv 12/08/2021
        /// <returns>số hàng được thêm thành công</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                // Kiểm tra mã nhân viên

                if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
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
                    !Regex.IsMatch(employee.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
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
                employee.EmployeeId = Guid.NewGuid();

                /// complete sql String
                var colNames = string.Empty;
                var colParams = string.Empty;
                // đọc từng property
                var properties = employee.GetType().GetProperties();

                DynamicParameters parameters = new DynamicParameters();
                // duyệt từng property

                foreach (var prop in properties)
                {
                    // lấy tên prop
                    var propName = prop.Name;
                    // lấy value prop
                    var propValue = prop.GetValue(employee);
                    // lấy kiểu prop
                    var propType = prop.PropertyType;

                    parameters.Add($"@{propName}", propValue);
                    colNames += $"{propName},";
                    colParams += $"@{propName},";
                }
                colNames = colNames.Remove(colNames.Length - 1, 1);
                colParams = colParams.Remove(colParams.Length - 1, 1);
                var sql = $"insert into Employee({colNames}) values( {colParams} ) ";
                var rowAffects = dbConnection.Execute(sql, param: parameters);
                var response = StatusCode(201, rowAffects);
                return response;
            }
            catch (Exception ex)
            {
                // lỗi trùng mã nhân viên (khi exception trả về chứa Duplicate)
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

        #region Sửa thông tin nhân viên theo id đc chọn

        /// <summary>
        /// Cập nhật một khách hàng với CustomerId trước
        /// </summary>
        /// <param name="EmployeeIdStr"> lấy từ route</param>
        /// <param name="employee"> lấy từ body</param>
        /// <returns>1 nếu sửa thành công và 0 là ngược lại</returns>
        /// Author hieunv 12/08/2021
        [HttpPut("{EmployeeIdStr}")]
        public IActionResult Put([FromRoute] string EmployeeIdStr, [FromBody] Employee employee)
        {

            try
            {
                Guid EmployeeId;
                // kiểm tra xem id truyền lên có đúng là guid hay chưa
                try
                {
                    EmployeeId = Guid.Parse(EmployeeIdStr);
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

                if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
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
                    !Regex.IsMatch(employee.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
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
                var properties = employee.GetType().GetProperties();

                DynamicParameters parameters = new DynamicParameters();
                // duyệt từng property

                foreach (var prop in properties)
                {
                    // lấy tên prop
                    var propName = prop.Name;


                    // lấy value prop
                    var propValue = prop.GetValue(employee);

                    // lấy kiểu prop
                    var propType = prop.PropertyType;

                    parameters.Add($"@{propName}", propValue);

                    cols += $" { propName } = @{propName},";

                }
                cols = cols.Remove(cols.Length - 1, 1);
                var sql = $"update Employee set {cols} where EmployeeId = {EmployeeId}";
                var rowAffects = dbConnection.Execute(sql, param: parameters);

                var response = StatusCode(200, rowAffects);
                return response;
            }
            catch (Exception ex)
            {
                // lỗi trùng mã nhân viên (khi exception trả về chứa Duplicate)
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

        #region Xóa một nhân viên theo id

        /// <summary>
        /// Xóa một khách hàng theo CustomerId
        /// </summary>
        /// <param name="EmployeeIdStr"></param>
        /// <returns>1 nếu xóa thành công và 0 là ngược lại</returns>
        [HttpDelete("{EmployeeIdStr}")]
        public IActionResult Delete(string EmployeeIdStr)
        {

            try
            {
                Guid EmployeeId;
                // kiểm tra xem id truyền lên có đúng là guid hay chưa
                try
                {
                    EmployeeId = Guid.Parse(EmployeeIdStr);
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

                var sql = $"delete from Employee where EmployeeId = {EmployeeId}";
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

