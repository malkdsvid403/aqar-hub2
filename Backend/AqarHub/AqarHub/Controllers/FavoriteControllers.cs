using Microsoft.AspNetCore.Mvc;
using RealEstate.BL.Services;
using RealEstate.DAL.Models;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoriteService _service;

        public FavoritesController(FavoriteService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(Favorite fav)
        {
            fav.CreatedAt = DateTime.Now;
            _service.Add(fav);
            return Ok("Added to favorites");
        }

        [HttpDelete("{userId}/{propertyId}")]
        public IActionResult Remove(int userId, int propertyId)
        {
            _service.Remove(userId, propertyId);
            return Ok("Removed from favorites");
        }

        [HttpGet("{userId}")]
        public IActionResult GetByUser(int userId)
        {
            return Ok(_service.GetByUser(userId));
        }
    }
}
