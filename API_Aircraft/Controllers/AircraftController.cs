

using System.Collections.Generic;
using API_Aircraft.Models;
using API_Aircraft.Service;
using Microsoft.AspNetCore.Mvc;

namespace API_Aircraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        #region Attribute
        private readonly AircraftService _aircraftService;
        private readonly DeletedAircraftService _deletedAircraftService;
        #endregion

        #region Method
        public AircraftController(AircraftService aircraftService, DeletedAircraftService deletedAirCraftService)
        {
            _aircraftService = aircraftService;
            _deletedAircraftService = deletedAirCraftService;
        }
        #endregion

        #region Read All AirCraft - OK
        [HttpGet]
        public ActionResult<List<Aircraft>> GetAllAircraft() => _aircraftService.GetAllAircraft();
        #endregion

        #region Read AirCraft One From RAB - OK
        [HttpGet("{rab:length(6)}", Name = "GetAirCraft")]
        public ActionResult<Aircraft> GetByAircraft(string rab)
        {
            var aircraft = _aircraftService.GetByAircraft(rab);

            if (aircraft == null)
                return NotFound();

            return Ok(aircraft);
        }
        #endregion

        #region Insert AirCraft - OK FALTA VALIDACAO PREFIXO
        [HttpPost]
        public ActionResult<Aircraft> CreateAircraft(Aircraft aircraft) //string cnpj
        {
            // aircraft.CNPJ = _companyService.GetByName(cnpj).CNPJ;

            //AirCraft airCraft = new AirCraft() { DtRegistry = System.DateTime.Now };

            aircraft.RAB = aircraft.RAB.Trim().ToLower();
            var rab = aircraft.RAB;
            aircraft.RAB = rab.Substring(0, 2) + "-" + rab.Substring(2, 3);

            // aircraft.DtRegistry = System.DateTime.Now;

            _aircraftService.CreateAircraft(aircraft);
            return CreatedAtRoute("GetAirCraft", new { rab = aircraft.RAB.ToString() }, aircraft);
        }
        #endregion

        #region AirCraft Update - OK
        [HttpPut("{rab:length(6)},{newCapacity}")]
        public ActionResult<Aircraft> UpdateAircraft(string rab, Aircraft aircraftIn, int newCapacity)
        {
            var aircraft = _aircraftService.GetByAircraft(rab);

            if (aircraft == null)
                return NotFound("Aeronave não encontrada!");

            if (rab != aircraftIn.RAB)
                return BadRequest("Não é possivel alterar o RAB!");

            if (aircraftIn.DtRegistry != aircraft.DtRegistry)
                return BadRequest("Não é possivel alterar a data do registro!");

            if (aircraftIn.DtLastFlight != aircraft.DtLastFlight)
                return BadRequest("Não é possivel alterar a data do último voo!");

            aircraftIn.Capacity = newCapacity;

            _aircraftService.UpdateAircraft(rab, aircraftIn);

            return NoContent();
        }
        #endregion

        #region Delete AirCraft - OK
        [HttpDelete("{rab:length(6)}")]
        public ActionResult RemoveAircraft(string rab)
        {
            var aircraft = _aircraftService.GetByAircraft(rab);

            if (aircraft == null)
                return NotFound();

            DeletedAircraft deleteAircraft = new DeletedAircraft();
            deleteAircraft.RAB = aircraft.RAB;
            deleteAircraft.Capacity = aircraft.Capacity;
            deleteAircraft.DtRegistry = aircraft.DtRegistry;
            deleteAircraft.DtLastFlight = aircraft.DtLastFlight;

            _deletedAircraftService.Create(deleteAircraft);
            _aircraftService.RemoveAircraft(aircraft);
            return NoContent();
        }
        #endregion
    }
}
