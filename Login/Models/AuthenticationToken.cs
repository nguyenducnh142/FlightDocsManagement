using Microsoft.EntityFrameworkCore;

namespace LoginService.Models
{

    [Keyless]
    public class AuthenticationToken
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
