using FlightDocsManagement.Models;

namespace FlightDocsManagement.Repository.Interface
{
    public interface IPilotCrewRepository
    {
        IEnumerable<Flight> GetFlights();
        IEnumerable<Docs> GetOriginDocsByFlightId(string flightId);
        IEnumerable<Docs> GetUpdateDocsByFlightId(string flightId);
        void UpdateDocs( Docs docs, string roleId);
        void UploadDocs( Docs docs, IFormFile file);
        void UpdateFileDocs(string docsName, IFormFile file, string roleId);
    }
}
