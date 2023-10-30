using FlightDocsManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsManagement.Repository.Interface
{
    public interface IPilotCrewRepository
    {
        IEnumerable<Flight> GetFlights();
        IEnumerable<Docs> GetOriginDocsByFlightId(string flightId);
        IEnumerable<Docs> GetUpdateDocsByFlightId(string flightId);
        IEnumerable<Docs> GetDocsByTypeId(string typeId);
        void UploadDocs(Docs docs, IFormFile file);
        void UpdateDocs( Docs docs, string roleId);
        void UpdateFileDocs(string docsName, IFormFile file, string roleId);
        Task<IActionResult> DownloadFile(string fileName);
    }
}
