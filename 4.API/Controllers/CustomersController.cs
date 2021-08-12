using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.api.Controllers
{
    /// <summary>
    /// api danh mục nhân viên
    /// Author hieunv 03/08/2021
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        IDbConnection dbConnection = DatabaseConnection.DbConnection;
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// Author hieunv 03/08/2021
        [HttpGet]
        public IActionResult Get()
        {
            var sql = "select * from Customer";
            var customers = dbConnection.Query<Customer>(sql);

            return Ok(customers);
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo id
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns>Khách hàng</returns>
        /// Author hieunv 03/08/2021
        [HttpGet("{CustomerId}")]
        public IActionResult Get(string CustomerId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", CustomerId);
            var sql = $"select * from Customer where CustomerId= @CustomerId";
            var customer = dbConnection.QueryFirstOrDefault(sql, param: parameters);
            return Ok(customer);
        }

        ///// <summary>
        ///// Lấy mã khách hàng
        ///// </summary>
        ///// <returns>mã khách hàng lớn nhất có trong csdl</returns>
        //[HttpGet("NewCustomerId")]
        //public IActionResult Get(int a)
        //{
        //    var sql = "select top 1 CustomerId from Customers order by CustomerId desc";
        //    var customer = dbConnection.QueryFirstOrDefault(sql);
        //    var newCustomerId = customer.CustomerId + 1;
        //    return Ok(newCustomerId);
        //}


        /// <summary>
        /// Thêm mới một khách hàng vào database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>số hàng được thêm thành công</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {

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

            var response = StatusCode(200, rowAffects);
            return response;
        }


        /// <summary>
        /// Cập nhật một khách hàng với CustomerId trước
        /// </summary>
        /// <param name="id"> lấy từ route</param>
        /// <param name="customer"> lấy từ body</param>
        /// <returns>1 nếu sửa thành công và 0 là ngược lại</returns>
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] string id, [FromBody] Customer customer)
        {
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
            var sql = $"update Customers set {cols} where CustomerId = {id}";
            var rowAffects = dbConnection.Execute(sql, param: parameters);

            var response = StatusCode(200, rowAffects);
            return response;
        }

        /// <summary>
        /// Xóa một khách hàng theo CustomerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1 nếu xóa thành công và 0 là ngược lại</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var sql = $"delete from Customers where CustomerId = {id}";
            var rowAffects = dbConnection.Execute(sql);
            var response = StatusCode(200, rowAffects);
            return response;
        }
    }
}
