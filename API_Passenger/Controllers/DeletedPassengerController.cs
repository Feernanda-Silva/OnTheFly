using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Passenger_API.Models;
using Passenger_API.Service;

namespace Passenger_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeletedPassengerController : ControllerBase
    {
        private readonly DeletedPassengerService _deletedPassengerService;
        public DeletedPassengerController(DeletedPassengerService deletedPassengerService)
        {
            _deletedPassengerService = deletedPassengerService;
        }

        #region GET ALL
        [HttpGet]
        public ActionResult<List<DeletedPassenger>> Get() => _deletedPassengerService.Get();
        #endregion

        #region GET BY CPF
        [HttpGet("CPF/{cpf}", Name = "GetDeletedPassengerbyCPF")]
        public ActionResult<DeletedPassenger> Get(string cpf)
        {
            cpf = FormatCPF(cpf);
            var passenger = _deletedPassengerService.Get(cpf);
            if (passenger == null)
                return NotFound();
            return Ok(passenger);
        }
        #endregion

        #region VALIDATORS
        public static string FormatCPF(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
        #endregion
    }
}
