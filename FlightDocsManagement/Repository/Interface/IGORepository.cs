using FlightDocsManagement.Models;

namespace FlightDocsManagement.Repository.Interface
{
    public interface IGORepository
    {
        void AddDocs(string flightId, Docs docs, IFormFile file);
        void ChangeDocsTypeId(string docsName, int typeId);
        IEnumerable<Docs> GetAllDocs();
        IEnumerable<Flight> GetCurrentFlights();
        IEnumerable<Docs> GetDocsByFlightId(string flightId);
        IEnumerable<Flight> GetFlights();
        void AddDocsType(DocsType docsType);
        void ChangeDocsTypeName(string typeId, string typeName);
    }
}
