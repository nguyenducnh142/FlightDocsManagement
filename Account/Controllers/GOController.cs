using Account.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemManage.Repository.Interface;

namespace SystemManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GOController : ControllerBase
    {
        private readonly IGORepository _gORepository;
        public GOController(IGORepository gORepository)
        {
            _gORepository = gORepository;
        }

        //Get Flights
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var subjects = _gORepository.GetUsers();
            return new OkObjectResult(subjects);
        }

        //Add Role
        [HttpPost("AddRole")]
        public IActionResult AddRole([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest("Role is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.AddRole(role);
            return new OkObjectResult(role.RoleName);
        }

        //Update RoleName
        [HttpPut("UpdateRoleName/{roleId}")]
        public IActionResult UpdateRoleName(string roleId, string roleName)
        {
            if (roleId == null || roleName == null)
            {
                return BadRequest("Role is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.UpdateRoleName(roleId, roleName);
            return new OkObjectResult(roleName);
        }

        //Delete Role
        [HttpDelete("DeleteRole/{roleId}")]
        public IActionResult DeleteRole(string roleId)
        {
            if (roleId == null)
            {
                return BadRequest("Role is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.DeleteRole(roleId);
            return new OkObjectResult(roleId);
        }

        //Change User's Status
        [HttpPut("ChangeUserStatus/{userId}")]
        public IActionResult ChangeUserStatus(string userId)
        {
            if (userId == null)
            {
                return BadRequest("User is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.ChangeUserStatus(userId);
            return new OkObjectResult(userId);
        }

        //Add User
        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.AddUser(user);
            return new OkObjectResult(user);
        }

        //Reset User's Password
        [HttpPut("ResetUserPassword/{userId}")]
        public IActionResult ResetUserPassword(string userId)
        {
            if (userId == null)
            {
                return BadRequest("User is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.ResetUserPassword(userId);
            return new OkObjectResult(userId);
        }

        //Change User's Role
        [HttpPut("ChangeUserRole/{userId}/{roleId}")]
        public IActionResult ChangeUserRole(string userId, string roleId)
        {
            if (userId == null || roleId == null)
            {
                return BadRequest("User is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _gORepository.ChangeUserRole(userId, roleId);
            return new OkObjectResult(userId);
        }

        //Get Roles
        [HttpGet("GetRoles")]
        public IActionResult GetRoles()
        {
            var subjects = _gORepository.GetRoles();
            return new OkObjectResult(subjects);
        }
    }
}
