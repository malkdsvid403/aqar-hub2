using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class FavoriteService
    {
        private readonly FavoriteRepository _repo;

        public FavoriteService(FavoriteRepository repo)
        {
            _repo = repo;
        }

        public int Add(Favorite fav) => _repo.Add(fav);

        public int Remove(int userId, int propertyId) => _repo.Remove(userId, propertyId);

        public List<Favorite> GetByUser(int userId) => _repo.GetByUser(userId);
    }
}
