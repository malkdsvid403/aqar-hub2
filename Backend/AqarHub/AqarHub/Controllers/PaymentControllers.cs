using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _service;

        public PaymentsController(PaymentService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(Payment payment)
        {
            payment.PaymentDate = DateTime.Now;
            _service.Add(payment);
            return Ok("Payment added successfully");
        }

        // GET ALL PAYMENTS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        // GET PAYMENTS BY USER
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            return Ok(_service.GetByUser(userId));
        }
    }
}
