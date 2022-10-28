
using System.Collections.Generic;
using API_Aircraft.Models;
using API_Aircraft.Service;
using Microsoft.AspNetCore.Mvc;

namespace API_Aircraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeletedAircraftController : ControllerBase
    {
        #region Attribute
        private readonly DeletedAircraftService _deletedAircraftService;
        private readonly AircraftService _aircraftService;
        #endregion

        #region Method
        public DeletedAircraftController(DeletedAircraftService deletedAirCraftServie, AircraftService airCraftServie)
        {
            _deletedAircraftService = deletedAirCraftServie;
            _aircraftService = airCraftServie;
        }
        #endregion

        #region Insert Deleted Aircraft
        [HttpPost]
        public ActionResult<DeletedAircraft> PostAircraft(DeletedAircraft deletedAirCraft, string rab)
        {
            Aircraft aircraftIn = _aircraftService.GetByAircraft(rab);

            deletedAirCraft.RAB = aircraftIn.RAB;
            deletedAirCraft.Capacity = aircraftIn.Capacity;
            deletedAirCraft.DtRegistry = aircraftIn.DtRegistry;
            deletedAirCraft.DtLastFlight = aircraftIn.DtLastFlight;
            //deletedAirCraft.Company= aircraftIn.Company;

            _deletedAircraftService.Create(deletedAirCraft);
            return CreatedAtRoute("GetDeletedAirCraft", new { rab = aircraftIn.RAB.ToString() }, aircraftIn);
        }
        #endregion

        #region Get All Deleted Aircraft
        [HttpGet]
        public ActionResult<List<DeletedAircraft>> GetAllAircraft() => _deletedAircraftService.GetAllDeletedAircraft();
        #endregion

        #region Get Deleted Aircraft By RAB
        [HttpGet("{rab:length(6)}", Name = "GetDeletedAircraft")]
        public ActionResult<DeletedAircraft> GetDeletedAircraft(string rab)
        {
            var plane = _deletedAircraftService.GetDeletedAircraft(rab);
            if (plane == null) return NotFound();
            return Ok(plane);
        }
        #endregion
    }
}
