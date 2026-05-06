using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

public class PropertyService
{
    private readonly PropertyRepository _repo;

    public PropertyService(PropertyRepository repo)
    {
        _repo = repo;
    }

    public List<Property> GetAll() => _repo.GetAll();

    public int Add(Property p) => _repo.Add(p);

    public List<Property> GetByOwner(int ownerId) => _repo.GetByOwner(ownerId);

    public int Update(Property property)
    {
        return _repo.Update(property);
    }

    public List<Property> Search(int? cityId, int? districtId, decimal? minPrice, decimal? maxPrice, int? typeId, int? rooms, bool? furnished)
    {
        return _repo.Search(cityId, districtId, minPrice, maxPrice, typeId, rooms, furnished);
    }


    public Property GetById(int id)
    {
        return _repo.GetById(id);
    }

    public List<Property> GetFeatured() => _repo.GetFeatured();

    public List<Property> GetLatest(int count) => _repo.GetLatest(count);


    public List<Property> GetPaged(int page, int pageSize) => _repo.GetPaged(page, pageSize);

    public int Delete(int id) => _repo.Delete(id);
}
