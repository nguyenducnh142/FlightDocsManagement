using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlightDocsManagement.Models
{
    public class DocsType
    {
        [Key]
        public string TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
