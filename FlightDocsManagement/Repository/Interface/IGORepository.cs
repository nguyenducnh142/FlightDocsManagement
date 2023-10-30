using FlightDocsManagement.Models;

namespace FlightDocsManagement.Repository.Interface
{
    public interface IGORepository
    {
        IEnumerable<Flight> GetFlights();
        IEnumerable<Flight> GetCurrentFlights();
        IEnumerable<Docs> GetAllDocs();
        IEnumerable<Docs> GetDocsByFlightId(string flightId);
        IEnumerable<Docs> GetDocsByTypeId(string typeId);
        void UploadDocs(Docs docs, IFormFile file);
        void UpdateDocs(Docs docs);
        void UpdateFileDocs(string docsName, IFormFile file);
        void AddDocsType(DocsType docsType);
        void ChangeDocsType(string docsName, string typeId);
        void ChangeTypeName(string typeId, string typeName);
    }
}
