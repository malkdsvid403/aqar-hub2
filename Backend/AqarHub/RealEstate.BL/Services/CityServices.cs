using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class CityService
    {
        private readonly CityRepository _repo;

        public CityService(CityRepository repo)
        {
            _repo = repo;
        }

        public List<City> GetAll() => _repo.GetAll();

        public int Add(City city) => _repo.Add(city);
    }
}
