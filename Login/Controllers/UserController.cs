using LoginService.Models;
using LoginService.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LoginService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Login
        [HttpPost("/Login")]
        public IActionResult Login([FromBody] Login user)
        {
            var loginResult = _userRepository.GenerateAuthToken(user);

            return loginResult is null ? Unauthorized() : Ok(loginResult);
        }
    }
}
