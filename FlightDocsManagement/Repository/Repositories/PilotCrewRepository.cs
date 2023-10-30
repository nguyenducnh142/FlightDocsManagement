using FlightDocsManagement.DbContexts;
using FlightDocsManagement.Models;
using FlightDocsManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsManagement.Repository.Repositories
{
    public class PilotCrewRepository : IPilotCrewRepository
    {
        private readonly FlightDocsMngContext _dbContext;

        public PilotCrewRepository(FlightDocsMngContext dbContext)
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

        public IEnumerable<Docs> GetOriginDocsByFlightId(string flightId)
        {
            return _dbContext.Docs.Where(d => d.FlightId == flightId && d.Version == "0").ToList();
        }

        public IEnumerable<Docs> GetUpdateDocsByFlightId(string flightId)
        {
            return _dbContext.Docs.Where(d => d.FlightId == flightId && d.Version != "0").ToList();
        }

        public void UploadDocs(Docs docs, IFormFile file)
        {
            var flight = _dbContext.Flights.FirstOrDefault(f => f.FlightId == docs.FlightId);
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }
            if (flight.FlightStatus == "Completed")
            {
                throw new Exception("Flight is completed");
            }
            _dbContext.Docs.Add(docs);
            var fileName = docs.DocsName + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Save();
        }

        public void UpdateDocs(Docs docs, string roleId)
        {
            var user = _dbContext.DocsPermissions.FirstOrDefault(u => u.RoleId == roleId && u.DocsName == docs.DocsName);
            if (user == null)
            {
                throw new Exception("You don't have permission to edit this file");
            }
            var flight = _dbContext.Flights.FirstOrDefault(f => f.FlightId == docs.FlightId);
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }
            if (flight.FlightStatus == "Completed")
            {
                throw new Exception("Flight is completed");
            }
            var docsUpdate = _dbContext.Docs.FirstOrDefault(d => d.DocsName == docs.DocsName);
            if (docsUpdate == null)
            {
                throw new Exception("Docs not found");
            }
            {
                docsUpdate.Version = docs.Version;
                docsUpdate.DocsName = docs.DocsName;
                docsUpdate.TypeId = docs.TypeId;
                docsUpdate.Notes = docs.Notes;
                docsUpdate.FlightId = docs.FlightId;
                docsUpdate.UpdateDate = DateTime.Now;
                _dbContext.Docs.Update(docsUpdate);
            }
            Save();
        }

        public void UpdateFileDocs( string docsName, IFormFile file, string roleId)
        {
            var user = _dbContext.DocsPermissions.FirstOrDefault(u => u.RoleId == roleId && u.DocsName == docsName);
            if (user == null)
            {
                throw new Exception("You don't have permission to edit this file");
            }
            var doc = _dbContext.Docs.FirstOrDefault(d => d.DocsName == docsName);
            if (doc == null)
            {
                throw new Exception("doc not found");
            }
            var flight = _dbContext.Flights.FirstOrDefault(f => f.FlightId == doc.FlightId);
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }
            if (flight.FlightStatus == "Completed")
            {
                throw new Exception("Flight is completed");
            }
            var fileName = docsName + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Save();
        }

        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", fileName);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return new FileStreamResult(memory, "application/pdf");
        }

        public IEnumerable<Docs> GetDocsByTypeId(string typeId)
        {
            return _dbContext.Docs.Where(d => d.TypeId == typeId).ToList();
        }
    }
}
