using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypeController : ControllerBase
    {
        private readonly PropertyTypeService _service;

        public PropertyTypeController(PropertyTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            return Ok(_service.Search(name));
        }

        [HttpGet("count/{typeId}")]
        public IActionResult CountPropertiesByType(int typeId)
        {
            return Ok(_service.CountPropertiesByType(typeId));
        }

        [HttpPut]
        public IActionResult Update(PropertyType type)
        {
            _service.Update(type);
            return Ok("Property type updated successfully");
        }


        [HttpPost]
        public IActionResult Add(PropertyType type)
        {
            _service.Add(type);
            return Ok("Property type added successfully");
        }
    }
}
