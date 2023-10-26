using FlightDocsManagement.Models;
using FlightDocsManagement.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GOController : ControllerBase
    {
        private readonly IGORepository _gORepository;
        public GOController(IGORepository gORepository)
        {
            _gORepository = gORepository;
        }

        //Get Flights
        [HttpGet("GetFlights")]
        public IActionResult GetFlights()
        {
            var subjects = _gORepository.GetFlights();
            return new OkObjectResult(subjects);
        }

        //Get Current Flights
        [HttpGet("GetCurrentFlights")]
        public IActionResult GetCurrentFlights()
        {
            var subjects = _gORepository.GetCurrentFlights();
            return new OkObjectResult(subjects);
        }
        //Get Docs by FlightId  
        [HttpGet("GetDocsByFlightId/{flightId}")]
        public IActionResult GetDocsByFlightId(string flightId)
        {
            var subjects = _gORepository.GetDocsByFlightId(flightId);
            return new OkObjectResult(subjects);
        }

        //Add file docs
        [HttpPost("AddDocs/{flightId}")]
        public IActionResult AddDocs(string flightId, [FromForm] Docs docs, IFormFile file)
        {
            if (docs == null)
            {
                return BadRequest("Docs is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.AddDocs(flightId, docs, file);
            return new OkObjectResult(flightId);
        }

        //Get All Docs
        [HttpGet("GetAllDocs")]
        public IActionResult GetAllDocs()
        {
            var docs = _gORepository.GetAllDocs();
            return new OkObjectResult(docs);
        }

        //Change Docs's TypeId
        [HttpPut("ChangeDocsType/{docsName}/{typeId}")]
        public IActionResult ChangeDocsTypeId(string docsName, int typeId)
        {
            _gORepository.ChangeDocsTypeId(docsName, typeId);
            return new OkObjectResult(docsName);
        }

        //Add DocsType
        [HttpPost("AddDocsType")]
        public IActionResult AddDocsType(DocsType docsType)
        {
            if (docsType == null)
            {
                return BadRequest("DocsType is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.AddDocsType(docsType);
            return new OkObjectResult(docsType);
        }

        //Change DocsType's TypeName
        [HttpPut("ChangeDocsTypeName/{typeId}/{typeName}")]
        public IActionResult ChangeDocsTypeName(string typeId, string typeName)
        {
            _gORepository.ChangeDocsTypeName(typeId, typeName);
            return new OkObjectResult(typeId);
        }
    }
}
    