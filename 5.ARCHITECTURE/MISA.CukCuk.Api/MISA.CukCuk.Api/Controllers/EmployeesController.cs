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
   
    public class EmployeesController : BaseEntityController<Employee>
    {
        IEmployeeService _employeeService;

        public EmployeesController(IBaseService<Employee> baseService, IEmployeeService employeeService) : base(baseService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("filter")]
        public IActionResult GetFitler([FromQuery] int pageSize, [FromQuery] int pageNumber,
            [FromQuery] Guid? positionId, [FromQuery] Guid? departmentId , [FromQuery] string searchTerms)
        {
            var serviceResult = _employeeService.GetFilter(pageSize, pageNumber, positionId, departmentId, searchTerms);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        //public override IActionResult Post([FromBody] Employee employee)
        //{
        //    var serviceResult = _employeeService.Add(employee);
        //    return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        //}

        //public override IActionResult Put([FromRoute] Guid employeeId, [FromBody] Employee employee)
        //{
        //    var serviceResult = _employeeService.Update(employee, employeeId);
        //    return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        //}
    }
}
