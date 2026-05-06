using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class RoleService
    {
        private readonly RoleRepository _repo;

        public RoleService(RoleRepository repo)
        {
            _repo = repo;
        }

        public List<Role> GetAll() => _repo.GetAll();

        public Role GetById(int id) => _repo.GetById(id);

        public int Add(Role role) => _repo.Add(role);

        public int CountUsersInRole(int roleId) => _repo.CountUsersInRole(roleId);

        public List<RoleWithCount> GetRolesWithUserCount() => _repo.GetRolesWithUserCount();

        public bool Exists(string name) => _repo.Exists(name);

        public List<Role> Search(string name) => _repo.Search(name);

        public int Delete(int id) => _repo.Delete(id);

        public int Update(Role role)
        {
            if (string.IsNullOrWhiteSpace(role.RoleName))
                throw new Exception("Role name is required");

            return _repo.Update(role);
        }
    }
}
