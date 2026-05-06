using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OwnerReviewsController : ControllerBase
{
    private readonly OwnerReviewService _service;

    public OwnerReviewsController(OwnerReviewService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Add(OwnerReview review)
    {
        review.CreatedAt = DateTime.Now;   // يمنع SqlDateTime overflow

        _service.Add(review);
        return Ok("Review added successfully");
    }

    [HttpGet("{ownerId}")]
    public IActionResult GetForOwner(int ownerId)
    {
        return Ok(_service.GetForOwner(ownerId));
    }
}
