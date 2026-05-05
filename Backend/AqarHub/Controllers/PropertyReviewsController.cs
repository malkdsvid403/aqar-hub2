using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PropertyReviewsController : ControllerBase
{
    private readonly PropertyReviewService _service;

    public PropertyReviewsController(PropertyReviewService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Add(PropertyReview review)
    {
        review.CreatedAt = DateTime.Now;
        _service.Add(review);
        return Ok("Review added successfully");
    }

    [HttpDelete("{reviewId}")]
    public IActionResult Delete(int reviewId)
    {
        _service.Delete(reviewId);
        return Ok("Review deleted successfully");
    }

    [HttpGet("count/{propertyId}")]
    public IActionResult CountReviews(int propertyId)
    {
        return Ok(_service.CountReviews(propertyId));
    }


    [HttpGet("average/{propertyId}")]
    public IActionResult GetAverageRating(int propertyId)
    {
        return Ok(_service.GetAverageRating(propertyId));
    }


    [HttpPut]
    public IActionResult Update(PropertyReview review)
    {
        _service.Update(review);
        return Ok("Review updated successfully");
    }


    [HttpGet("{propertyId}")]
    public IActionResult GetByProperty(int propertyId)
    {
        return Ok(_service.GetByProperty(propertyId));
    }
}
