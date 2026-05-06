using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class PropertyTypeService
    {
        private readonly PropertyTypeRepository _repo;

        public PropertyTypeService(PropertyTypeRepository repo)
        {
            _repo = repo;
        }

        public List<PropertyType> GetAll() => _repo.GetAll();

        public int Update(PropertyType type) => _repo.Update(type);

        public List<PropertyType> Search(string name) => _repo.Search(name);

        public int CountPropertiesByType(int typeId) => _repo.CountPropertiesByType(typeId);


        public int Add(PropertyType type) => _repo.Add(type);
    }
}
