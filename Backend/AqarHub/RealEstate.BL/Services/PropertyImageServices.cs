using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class PropertyImageService
    {
        private readonly PropertyImageRepository _repo;

        public PropertyImageService(PropertyImageRepository repo)
        {
            _repo = repo;
        }

        public List<PropertyImage> GetByProperty(int propertyId)
        {
            return _repo.GetByProperty(propertyId);
        }

        public int Add(PropertyImage img)
        {
            return _repo.Add(img);
        }

        public int Delete(int imageId)
        {
            return _repo.Delete(imageId);
        }
    }
}
