using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : ControllerBase
    {
        private readonly PropertyImageService _service;

        public PropertyImageController(PropertyImageService service)
        {
            _service = service;
        }

        [HttpGet("{propertyId}")]
        public IActionResult GetByProperty(int propertyId)
        {
            return Ok(_service.GetByProperty(propertyId));
        }

        [HttpPost]
        public IActionResult Add(PropertyImage img)
        {
            _service.Add(img);
            return Ok("Image added successfully");
        }

        [HttpDelete("{imageId}")]
        public IActionResult Delete(int imageId)
        {
            _service.Delete(imageId);
            return Ok("Image deleted successfully");
        }
    }
}
