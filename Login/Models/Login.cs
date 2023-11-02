using Microsoft.EntityFrameworkCore;

namespace LoginService.Models
{
    [Keyless]
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
