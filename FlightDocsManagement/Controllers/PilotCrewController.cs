using FlightDocsManagement.Models;
using FlightDocsManagement.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotCrewController : ControllerBase
    {
        private readonly IPilotCrewRepository _pilotCrewRepository;
        public PilotCrewController(IPilotCrewRepository pilotCrewRepository)
        {
            _pilotCrewRepository = pilotCrewRepository;
        }

        //Get Flights
        [HttpGet("GetFlights")]
        public IActionResult GetFlights()
        {
            var subjects = _pilotCrewRepository.GetFlights();
            return new OkObjectResult(subjects);
        }

        //Get Docs by FlightId with Docs's Version = 0
        [HttpGet("GetOriginDocsByFlightId/{flightId}")]
        public IActionResult GetOriginDocsByFlightId(string flightId)
        {
            var subjects = _pilotCrewRepository.GetOriginDocsByFlightId(flightId);
            return new OkObjectResult(subjects);
        }

        //Get Docs by FlightId with Docs's Version != 0
        [HttpGet("GetUpdateDocsByFlightId/{flightId}")]
        public IActionResult GetUpdateDocsByFlightId(string flightId)
        {
            var subjects = _pilotCrewRepository.GetUpdateDocsByFlightId(flightId);
            return new OkObjectResult(subjects);
        }

        //Upload Docs
        [HttpPost("UploadDocs/{flightId}")]
        public IActionResult UploadDocs([FromForm] Docs docs, [FromForm] IFormFile file)
        {
            try
            {
                _pilotCrewRepository.UploadDocs(docs, file);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //Update Docs
        [HttpPut("UpdateDocs/{flightId}")]
        public IActionResult UpdateDocs( [FromForm] Docs docs)
        {
            try
            {
                _pilotCrewRepository.UpdateDocs(docs);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}
