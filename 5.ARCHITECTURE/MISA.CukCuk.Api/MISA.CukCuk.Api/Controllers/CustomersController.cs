using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;


        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        #region Lấy toàn bộ ds khách hàng

        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// Author hieunv 14/08/2021
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var serviceResult = _customerService.Get();
                return StatusCode(serviceResult.StatusCode, serviceResult.Data);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Resources.ResourceVN.MISA_Error_User,
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
        /// Author hieunv 14/08/2021
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                var serviceResult = _customerService.Post(customer);

                if (serviceResult.IsValid)
                {
                    return StatusCode(serviceResult.StatusCode, serviceResult.Data);
                }
                else
                {
                    return StatusCode(serviceResult.StatusCode, serviceResult.Data);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Resources.ResourceVN.MISA_Error_User
                };
                return StatusCode(500, errorObj);
            }
        }

        #endregion
    }
}
