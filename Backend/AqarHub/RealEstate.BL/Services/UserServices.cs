using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public List<User> GetAll() => _repo.GetAll();

        public int Update(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
                throw new Exception("First name is required");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new Exception("Email is required");

            return _repo.Update(user);
        }
        public UserProfileDto GetUserProfile(int userId)
        {
            return _repo.GetUserProfile(userId);
        }


        public List<User> GetUsersByRole(int roleId)
        {
            return _repo.GetUsersByRole(roleId);
        }


        public int ChangePassword(int userId, string newPassword)
        {
            return _repo.ChangePassword(userId, newPassword);
        }

        public List<User> Search(string keyword)
        {
            return _repo.Search(keyword);
        }


        public User GetById(int id)
        {
            return _repo.GetById(id);
        }


        public int Add(User user) => _repo.Add(user);

        public User Login(string email, string password) => _repo.Login(email, password);

        public int Delete(int id) => _repo.Delete(id);
    }
}
