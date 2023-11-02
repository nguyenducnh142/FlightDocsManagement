using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;

namespace LoginService.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public string Email { get; set; } 
        public bool Status { get; set; }

    }
}

