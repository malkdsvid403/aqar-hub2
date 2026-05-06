using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class PlanService
    {
        private readonly PlanRepository _repo;

        public PlanService(PlanRepository repo)
        {
            _repo = repo;
        }

        public int Add(Plan plan) => _repo.Add(plan);
        public List<Plan> GetAll() => _repo.GetAll();
        public int Delete(int id) => _repo.Delete(id);
        public int Update(Plan plan) => _repo.Update(plan);
    }

}
