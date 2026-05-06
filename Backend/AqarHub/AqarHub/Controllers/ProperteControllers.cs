using Microsoft.AspNetCore.Mvc;
using RealEstate.DAL.Models;

[Route("api/[controller]")]
[ApiController]
public class PropertiesController : ControllerBase
{
    private readonly PropertyService _service;

    public PropertiesController(PropertyService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpPost]
    public IActionResult Add(Property p)
    {
        _service.Add(p);
        return Ok("Property added successfully");
    }

    [HttpGet("owner/{ownerId}")]
    public IActionResult GetByOwner(int ownerId)
    {
        return Ok(_service.GetByOwner(ownerId));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return Ok("Property deleted");
    }

    [HttpPut]
    public IActionResult Update(Property property)
    {
        _service.Update(property);
        return Ok("Property updated successfully");
    }

    [HttpGet("search")]
    public IActionResult Search(
    int? cityId,
    int? districtId,
    decimal? minPrice,
    decimal? maxPrice,
    int? typeId,
    int? rooms,
    bool? furnished)
    {
        return Ok(_service.Search(cityId, districtId, minPrice, maxPrice, typeId, rooms, furnished));
    }


    [HttpGet("featured")]
    public IActionResult GetFeatured()
    {
        return Ok(_service.GetFeatured());
    }

    [HttpGet("latest/{count}")]
    public IActionResult GetLatest(int count)
    {
        return Ok(_service.GetLatest(count));
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var property = _service.GetById(id);
        if (property == null)
            return NotFound("Property not found");

        return Ok(property);
    }


    [HttpGet("paged")]
    public IActionResult GetPaged(int page = 1, int pageSize = 10)
    {
        return Ok(_service.GetPaged(page, pageSize));
    }

}
