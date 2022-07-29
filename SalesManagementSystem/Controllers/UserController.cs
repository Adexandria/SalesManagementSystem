using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Model;
using SalesManagementSystem.Services;

namespace SalesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            bool usernameExist = _user.IsUsernameExist(user.Username);
            if (!usernameExist)
            {
                _user.CreateUser(user);
                return Ok();
            }
            return BadRequest("This username already exist");
        }
        
        [HttpPut("{userId}/email")]
        public IActionResult UpdateUserEmail(Guid userId, string email)
        {
            bool exist = _user.IsUserExist(userId);
            if(exist)
            {
                _user.UpdateUserEmail(userId, email);
                return Ok();
            }
            return NotFound("User not found");
        }
        
        [HttpPut("{userId}/password")]
        public IActionResult UpdateUserPassword(Guid userId, string password)
        {
            bool exist = _user.IsUserExist(userId);
            if (exist)
            {
                _user.UpdateUserPassword(userId, password);
                return Ok();
            }
            return NotFound("User not found");
        }
        
        [HttpPut("{userId}/username")]
        public IActionResult UpdateUserName(Guid userId, string username)
        {
            bool exist = _user.IsUserExist(userId);
            if (exist)
            {
                _user.UpdateUserName(userId, username);
                return Ok();
            }
            return NotFound("User not found");
        }
        
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            bool exist = _user.IsUserExist(userId);
            if (exist)
            {
                _user.DeleteUser(userId);
                return Ok();
            }
            return NotFound("User not found");
        }
    }
}
