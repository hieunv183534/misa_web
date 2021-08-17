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
   
    public class CustomersController : BaseEntityController<Customer>
    {
        ICustomerService _customerService;

        public CustomersController(IBaseService<Customer> baseService, ICustomerService customerService) :base(baseService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Api lọc nhân viên theo tiêu chí và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="customerGroupId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult GetFitler([FromQuery] int pageSize, [FromQuery] int pageNumber,
            [FromQuery] Guid? customerGroupId, [FromQuery] string searchTerms)
        {
            var serviceResult = _customerService.GetFilter(pageSize, pageNumber, customerGroupId, searchTerms);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        ///// <summary>
        ///// Ghi đè api post của base
        ///// </summary>
        ///// <param name="customer"></param>
        ///// <returns></returns>
        //public override IActionResult Post([FromBody] Customer customer)
        //{
        //    var serviceResult = _customerService.Add(customer);
        //    return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        //}

        ///// <summary>
        ///// Ghi đè api put của base
        ///// </summary>
        ///// <param name="customerId"></param>
        ///// <param name="customer"></param>
        ///// <returns></returns>
        //public override IActionResult Put([FromRoute] Guid customerId, [FromBody] Customer customer)
        //{
        //    var serviceResult = _customerService.Update(customer, customerId);
        //    return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        //}

    }
}
