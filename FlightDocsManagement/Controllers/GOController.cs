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
        public IActionResult AddDocs( [FromForm] Docs docs, IFormFile file)
        {
            if (docs == null)
            {
                return BadRequest("Docs is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.UploadDocs(docs, file);
            return new OkObjectResult(docs.FlightId);
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
        public IActionResult ChangeDocsTypeId(string docsName, string typeId)
        {
            _gORepository.ChangeDocsType(docsName, typeId);
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
            _gORepository.ChangeTypeName(typeId, typeName);
            return new OkObjectResult(typeId);
        }

        //Update Docs
        [HttpPut("UpdateDocs")]
        public IActionResult UpdateDocs(Docs docs)
        {
            if (docs == null)
            {
                return BadRequest("Docs is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.UpdateDocs(docs);
            return new OkObjectResult(docs);
        }

        //Update File Docs
        [HttpPut("UpdateFileDocs/{docsName}")]
        public IActionResult UpdateFileDocs(string docsName, IFormFile file)
        {
            if (docsName == null)
            {
                return BadRequest("DocsName is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.UpdateFileDocs(docsName, file);
            return new OkObjectResult(docsName);
        }
    }
}
    