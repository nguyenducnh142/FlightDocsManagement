using FlightDocsManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsManagement.DbContexts
{
    public class FlightDocsMngContext : DbContext
    {
        public DbSet<Docs> Docs { get; set; }
        public DbSet<DocsType> DocsTypes { get; set; }
        public DbSet<Flight> Flights { get; set; }
    }
}
