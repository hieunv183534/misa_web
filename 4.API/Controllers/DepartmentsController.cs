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
    /// Api danh mục phòng ban
    /// Author hieunv 12/08/2021
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        IDbConnection dbConnection = DatabaseConnection.DbConnection;

        #region Lấy toàn bộ danh sách phòng ban

        /// <summary>
        /// Lấy toàn bộ danh sách phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Author hieunv 12/08/2021
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var sql = "select * from Department";
                var departments = dbConnection.Query<Department>(sql);
                if (departments.Count() > 0)
                {
                    return StatusCode(200, departments);
                }
                else
                {
                    return StatusCode(204, departments);
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
    }
}
