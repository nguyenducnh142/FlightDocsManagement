using FlightDocsManagement.DbContexts;
using FlightDocsManagement.Models;
using FlightDocsManagement.Repository.Interface;

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
                f => f.FlightDate.DayOfYear == DateTime.Today.DayOfYear && 
                f.FlightDate < DateTime.Now && 
                f.FlightDate.AddMinutes(f.FlightTime) >= DateTime.Now)
                .ToList();
        }

        public IEnumerable<Docs> GetDocsByFlightId(string flightId)
        {
            return _dbContext.Docs.Where(d => d.FlightId == flightId).ToList();
        }


        public IEnumerable<Docs> GetAllDocs()
        {
            return _dbContext.Docs.ToList();
        }

        public void ChangeDocsType(string docsName, string typeId)
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
        public void ChangeTypeName(string typeId, string typeName)
        {
            var docsType = _dbContext.DocsTypes.FirstOrDefault(d => d.TypeId == typeId);
            if (docsType == null)
            {
                throw new Exception("DocsType not found");
            }
            docsType.TypeName = typeName;
            Save();
        }
        public IEnumerable<Docs> GetDocsByTypeId(string typeId)
        {
            return _dbContext.Docs.Where(d => d.TypeId == typeId).ToList();
        }

        //Update Docs
        public void UpdateDocs(Docs docs)
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

        public void UpdateFileDocs(string docsName, IFormFile file)
        {
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
            doc.UpdateDate = DateTime.Now;
            var fileName = docsName + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Save();
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
            docs.UpdateDate = DateTime.Now;
            _dbContext.Docs.Add(docs);
            var fileName = docs.DocsName + Path.GetExtension(file.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = Path.Combine(path, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Save();
        }

        public void AddFlight(Flight flight)
        {
            _dbContext.Flights.Add(flight);
            Save();
        }

        public void AddDocsPermission(DocsPermission docsPermission)
        {
            _dbContext.DocsPermissions.Add(docsPermission);
            Save();
        }

        public void UpdateDocsPermission(DocsPermission docsPermission)
        {
            var docsPermissionUpdate = _dbContext.DocsPermissions.FirstOrDefault(d => d.DocsName == docsPermission.DocsName && d.RoleId == docsPermission.RoleId);
            if (docsPermissionUpdate == null)
            {
                throw new Exception("DocsPermission not found");
            }
            docsPermissionUpdate.RoleId = docsPermission.RoleId;
            docsPermissionUpdate.DocsName = docsPermission.DocsName;
            _dbContext.DocsPermissions.Update(docsPermissionUpdate);
            Save();
        }

        public void DeleteDocsPermission(DocsPermission docsPermission)
        {
            var docsPermissionDelete = _dbContext.DocsPermissions.FirstOrDefault(d => d.DocsName == docsPermission.DocsName && d.RoleId == docsPermission.RoleId);
            if (docsPermissionDelete == null)
            {
                throw new Exception("DocsPermission not found");
            }
            _dbContext.DocsPermissions.Remove(docsPermissionDelete);
            Save();
        }

        public IEnumerable<DocsPermission> GetDocsPermission()
        {
            return _dbContext.DocsPermissions.ToList();
        }
    }
}