using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocsManagement.Models
{
    public class Docs
    {
        [Key]
        public string DocsName { get; set; }
        public int TypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Version { get; set; }
        public string Notes { get; set; }
        public string Permisssion { get; set; }
        public string FlightId { get; set; }
    }
}
