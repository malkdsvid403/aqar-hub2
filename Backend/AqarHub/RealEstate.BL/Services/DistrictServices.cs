using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class DistrictService
    {
        private readonly DistrictRepository _repo;

        public DistrictService(DistrictRepository repo)
        {
            _repo = repo;
        }

        public List<District> GetAll() => _repo.GetAll();

        public int Add(District district) => _repo.Add(district);
    }
}
