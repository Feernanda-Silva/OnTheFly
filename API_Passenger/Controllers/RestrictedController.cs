using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Passenger_API.Models;
using Passenger_API.Service;
using System;
using System.Collections.Generic;

namespace Passenger_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictedController : ControllerBase
    {
        private readonly RestrictedService _restrictedService;
        public RestrictedController(RestrictedService restrictedService)
        {
            _restrictedService = restrictedService;
        }

        [HttpGet]
        public ActionResult<List<Restricted>> Get() => _restrictedService.Get();

        [HttpGet("CPF/{cpf}", Name = "GetRestrictedbyCPF")]
        public ActionResult<DeletedPassenger> Get(string cpf)
        {
            cpf = FormatCPF(cpf);
            var passenger = _restrictedService.Get(cpf);
            if (passenger == null)
                return NotFound();
            return Ok(passenger);
        }
        public static string FormatCPF(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
    }
}
