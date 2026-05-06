using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _service;

        public MessagesController(MessageService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Message msg)
        {
            if (msg == null)
                return BadRequest("Invalid message data");

            msg.SentAt = DateTime.Now;
            msg.IsRead = false;

            if (msg.PropertyId == 0)
                msg.PropertyId = null;

            _service.Add(msg);
            return Ok("Message sent");
        }

        [HttpGet("{u1}/{u2}")]
        public IActionResult GetConversation(int u1, int u2)
        {
            return Ok(_service.GetConversation(u1, u2));
        }
    }
}
