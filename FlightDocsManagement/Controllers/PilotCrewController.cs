using FlightDocsManagement.Models;
using FlightDocsManagement.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Claims;

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
        [HttpGet("getrole")]
        public string GetRoleId()
        {
            string id = Convert.ToString(HttpContext.User.FindFirstValue("RoleId"));
            return id;
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
        public IActionResult UpdateDocs([FromForm] Docs docs)
        {
            try
            {
                _pilotCrewRepository.UpdateDocs(docs, GetRoleId());
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Update File Docs
        [HttpPut("UpdateFileDocs/{docsName}")]
        public IActionResult UpdateFileDocs(string docsName, [FromForm] IFormFile file, string roleId)
        {
            try
            {
                _pilotCrewRepository.UpdateFileDocs(docsName, file, roleId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /*//Download File
        [HttpGet("DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            try
            {
                return await _pilotCrewRepository.DownloadFile(fileName);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

        //Download file
        [HttpGet("DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", fileName);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }

        //Get Docs by TypeId
        [HttpGet("GetDocsByTypeId/{typeId}")]
        public IActionResult GetDocsByTypeId(string typeId)
        {
            var subjects = _pilotCrewRepository.GetDocsByTypeId(typeId);
            return new OkObjectResult(subjects);
        }
        

    }
}
