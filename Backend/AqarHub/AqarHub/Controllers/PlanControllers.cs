using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly PlanService _service;

        public PlansController(PlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Add(Plan plan)
        {
            _service.Add(plan);
            return Ok("Plan added successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Plan deleted");
        }

        [HttpPut]
        public IActionResult Update(Plan plan)
        {
            _service.Update(plan);
            return Ok("Plan updated successfully");
        }
    }
}
