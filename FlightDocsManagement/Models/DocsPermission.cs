using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlightDocsManagement.Models
{
    [PrimaryKey(nameof(DocsName), nameof(RoleId))]
    public class DocsPermission
    {
        public string DocsName { get; set; }
        public string RoleId { get; set; }
    }
}
