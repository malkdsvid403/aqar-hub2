using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly DistrictService _service;

        public DistrictController(DistrictService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Add(District district)
        {
            _service.Add(district);
            return Ok("District added successfully");
        }
    }
}
