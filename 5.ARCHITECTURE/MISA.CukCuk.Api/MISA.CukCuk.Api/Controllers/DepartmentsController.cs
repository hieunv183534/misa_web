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
    public class DepartmentsController : BaseEntityController<Department>
    {
        IBaseService<Department> _baseService;

        public DepartmentsController(IBaseService<Department> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
    }
}
