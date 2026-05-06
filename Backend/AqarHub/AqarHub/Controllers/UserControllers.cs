using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            _service.Add(user);
            return Ok("User added successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest req)
        {
            var user = _service.Login(req.Email, req.Password);

            if (user == null)
                return Unauthorized("Invalid email or password");

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("User deleted");
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            _service.Update(user);
            return Ok("User updated successfully");

        }
        [HttpGet("by-role/{roleId}")]
        public IActionResult GetUsersByRole(int roleId)
        {
            return Ok(_service.GetUsersByRole(roleId));
        }

        [HttpGet("profile/{userId}")]
        public IActionResult GetUserProfile(int userId)
        {
            var profile = _service.GetUserProfile(userId);
            return profile == null ? NotFound() : Ok(profile);
        }


        [HttpPut("change-password")]
        public IActionResult ChangePassword(int userId, string newPassword)
        {
            _service.ChangePassword(userId, newPassword);
            return Ok("Password changed successfully");
        }


        [HttpGet("search")]
        public IActionResult Search(string keyword)
        {
            return Ok(_service.Search(keyword));
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _service.GetById(id);
            return user == null ? NotFound() : Ok(user);
        }


        public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}




}
