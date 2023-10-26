using FlightDocsManagement.DbContexts;
using FlightDocsManagement.Models;
using FlightDocsManagement.Repository.Interface;
using Microsoft.Extensions.Primitives;

namespace FlightDocsManagement.Repository.Repositories
{
    public class GORepository : IGORepository
    {
        private readonly FlightDocsMngContext _dbContext;

        public GORepository(FlightDocsMngContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<Flight> GetFlights()
        {
            return _dbContext.Flights.ToList();
        }

        public IEnumerable<Flight> GetCurrentFlights()
        {
            return _dbContext.Flights.Where(
                f => f.FlightDate.Date == DateTime.Today.Date && 
                f.FlightDate > DateTime.Now && 
                f.FlightDate <= DateTime.Now.AddMinutes(f.FlightTime))
                .ToList();
        }

        public IEnumerable<Docs> GetDocsByFlightId(string flightId)
        {
            return _dbContext.Docs.Where(d => d.FlightId == flightId).ToList();
        }

        public void AddDocs(string flightId, Docs docs, IFormFile file)
        {
            var flight = _dbContext.Flights.FirstOrDefault(f => f.FlightId == flightId);
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }
            if (flight.FlightStatus == "Completed")
            {
                throw new Exception("Flight is completed");
            }
            docs.FlightId = flightId;
            _dbContext.Docs.Add(docs);
            var fileName = docs.DocsName + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Save();
        }

        public IEnumerable<Docs> GetAllDocs()
        {
            return _dbContext.Docs.ToList();
        }

        public void ChangeDocsTypeId(string docsName, int typeId)
        {
            var docs = _dbContext.Docs.FirstOrDefault(d => d.DocsName == docsName);
            if (docs == null)
            {
                throw new Exception("Docs not found");
            }
            docs.TypeId = typeId;
            Save();
        }

        public void AddDocsType(DocsType docsType)
        {
            _dbContext.DocsTypes.Add(docsType);
            Save();
        }
        public void ChangeDocsTypeName(string typeId, string typeName)
        {
            var docsType = _dbContext.DocsTypes.FirstOrDefault(d => d.TypeId == typeId);
            if (docsType == null)
            {
                throw new Exception("DocsType not found");
            }
            docsType.TypeName = typeName;
            Save();
        }
       
    }
}