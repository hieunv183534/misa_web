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
    /// Api danh mục vi trí công việc
    /// Author hieunv 12/08/2021
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        IDbConnection dbConnection = DatabaseConnection.DbConnection;

        #region Lấy toàn bộ ds các vị trí công việc

        /// <summary>
        /// Lấy toàn bộ danh sách vi trí
        /// </summary>
        /// <returns>Danh sách vi trí</returns>
        /// Author hieunv 12/08/2021
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var sql = "select * from Position";
                var positions = dbConnection.Query<Position>(sql);
                if (positions.Count() > 0)
                {
                    return StatusCode(200, positions);
                }
                else
                {
                    return StatusCode(204, positions);
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
