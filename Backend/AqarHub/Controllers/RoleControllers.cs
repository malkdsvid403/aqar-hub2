using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _service;

        public RoleController(RoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Add(Role role)
        {
            _service.Add(role);
            return Ok("Role added successfully");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var role = _service.GetById(id);
            return role == null ? NotFound() : Ok(role);
        }

        [HttpPut]
        public IActionResult Update(Role role)
        {
            _service.Update(role);
            return Ok("Role updated successfully");
        }

        [HttpGet("with-count")]
        public IActionResult GetRolesWithUserCount()
        {
            return Ok(_service.GetRolesWithUserCount());
        }

        [HttpGet("count/{roleId}")]
        public IActionResult CountUsersInRole(int roleId)
        {
            return Ok(_service.CountUsersInRole(roleId));
        }

        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            return Ok(_service.Search(name));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Role deleted successfully");
        }
    }
}

