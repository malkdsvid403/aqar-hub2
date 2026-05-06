using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionsController : ControllerBase
    {
        private readonly UserSubscriptionService _service;

        public UserSubscriptionsController(UserSubscriptionService service)
        {
            _service = service;
        }

        // POST: Add subscription
        [HttpPost]
        public IActionResult Add(UserSubscription sub)
        {
            _service.Add(sub);
            return Ok("Subscription added successfully");
        }

        // GET: Get all subscriptions for a user
        [HttpGet("{userId}")]
        public IActionResult GetByUser(int userId)
        {
            return Ok(_service.GetByUser(userId));
        }

        // GET: Get active subscription for a user
        [HttpGet("active/{userId}")]
        public IActionResult GetActive(int userId)
        {
            var result = _service.GetActive(userId);
            return Ok(result);
        }

        // PUT: Deactivate subscription
        [HttpPut("deactivate/{id}")]
        public IActionResult Deactivate(int id)
        {
            _service.Deactivate(id);
            return Ok("Subscription deactivated");
        }

        [HttpPut("extend/{id}")]
        public IActionResult Extend(int id, int days)
        {
            _service.Extend(id, days);
            return Ok("Subscription extended successfully");
        }

        // 2) Upgrade subscription
        [HttpPut("upgrade/{id}")]
        public IActionResult Upgrade(int id, int newPlanId)
        {
            _service.Upgrade(id, newPlanId);
            return Ok("Subscription upgraded successfully");
        }

        // 3) Check if user has active subscription
        [HttpGet("has-active/{userId}")]
        public IActionResult HasActive(int userId)
        {
            var result = _service.HasActive(userId);
            return Ok(result);
        }

        // 4) Get subscription details (with plan info)
        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var result = _service.GetDetails(id);
            return Ok(result);
        }

        // 5) Auto deactivate expired subscriptions
        [HttpPut("auto-deactivate")]
        public IActionResult AutoDeactivate()
        {
            _service.AutoDeactivateExpired();
            return Ok("Expired subscriptions deactivated");
        }

        // 6) Get all subscriptions (Admin)
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        // DELETE: Delete subscription
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Subscription deleted");
        }
    }

}
