using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityService _service;

        public CityController(CityService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Add(City city)
        {
            _service.Add(city);
            return Ok("City added successfully");
        }
    }
}
