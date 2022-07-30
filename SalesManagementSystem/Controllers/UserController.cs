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
        

        //Create new user
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            bool usernameExist = _user.IsUsernameExist(user.Username);
            if (!usernameExist)
            {
                Guid userId = _user.CreateUser(user);
                return Ok($"User {userId} created successfully,");
            }
            return BadRequest("This username already exist");
        }
        
        [HttpPut("{userId}/email")]
        public IActionResult UpdateUserEmail(Guid userId, [FromBody] string email)
        {
            bool exist = _user.IsUserExist(userId);
            if(exist)
            {
                _user.UpdateUserEmail(userId, email);
                return Ok("Updated successfully");
            }
            return NotFound("User not found");
        }
        
        [HttpPut("{userId}/password")]
        public IActionResult UpdateUserPassword(Guid userId, [FromBody] string password)
        {
            bool exist = _user.IsUserExist(userId);
            if (exist)
            {
                _user.UpdateUserPassword(userId, password);
                return Ok("Updated successfully");
            }
            return NotFound("User not found");
        }
        
        [HttpPut("{userId}/username")]
        public IActionResult UpdateUserName(Guid userId, [FromBody] string username)
        {
            bool exist = _user.IsUserExist(userId);
            if (exist)
            {
                _user.UpdateUserName(userId, username);
                return Ok("Updated successfully");
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
                return Ok("Deleted successfully");
            }
            return NotFound("User not found");
        }
    }
}
