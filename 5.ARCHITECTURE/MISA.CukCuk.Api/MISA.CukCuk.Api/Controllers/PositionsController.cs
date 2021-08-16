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
   
    public class PositionsController : BaseEntityController<Position>
    {
        IBaseService<Position> _baseService;

        public PositionsController(IBaseService<Position> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
    }
}
