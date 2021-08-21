using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.IServices;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
   
    public class CustomersController : BaseEntityController<Customer>
    {
        #region Declare

        ICustomerService _customerService;

        #endregion

        #region Constructor
        public CustomersController(IBaseService<Customer> baseService, ICustomerService customerService) : base(baseService)
        {
            _customerService = customerService;
        }
        #endregion

        #region GetFitler

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

        #endregion

        #region Post

        /// <summary>
        /// Ghi đè api post của base
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public override IActionResult Post([FromBody] Customer customer)
        {
            var serviceResult = _customerService.Add(customer);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        #endregion

        #region Put

        /// <summary>
        /// Ghi đè api put của base
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public override IActionResult Put([FromRoute] Guid customerId, [FromBody] Customer customer)
        {
            var serviceResult = _customerService.Update(customer, customerId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        #endregion

        #region GetAllCustomer

        public override IActionResult GetAll()
        {
            var serviceResult = _customerService.GetAll();
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        #endregion

        #region Import

        /// <summary>
        /// import đọc file
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("import")]
        public IActionResult Import(IFormFile formFile)
        {
            try
            {
                // check file có hợp lệ hay không?
                if (formFile == null)
                {
                    return StatusCode(400, Core.Resources.ResourceVN.MISA_Error_Dev_NullField);
                }
                // check độ lớn của file
                // thực hiện đọc dữ liệu
                var customers = new List<Customer>();
                using (var stream = new MemoryStream())
                {
                    formFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var serviceResult = _customerService.Import(package);
                        return StatusCode(serviceResult.StatusCode, serviceResult.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        #endregion

    }
}
