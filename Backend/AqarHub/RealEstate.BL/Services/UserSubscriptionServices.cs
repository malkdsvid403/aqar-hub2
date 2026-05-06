using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;
using System.Data;

namespace RealEstate.BL.Services
{
    public class UserSubscriptionService
    {
        private readonly UserSubscriptionRepository _repo;

        public UserSubscriptionService(UserSubscriptionRepository repo)
        {
            _repo = repo;
        }

        public List<UserSubscription> GetByUser(int userId)
        {
            return _repo.GetByUser(userId);
        }

        public int Add(UserSubscription sub)
        {
            return _repo.Add(sub);
        }

        public UserSubscription GetActive(int userId) => _repo.GetActive(userId);

        public int Delete(int id) => _repo.Delete(id);

        public void Extend(int id, int days)
        {
            _repo.Extend(id, days);
        }

        public void Upgrade(int id, int newPlanId)
        {
            _repo.Upgrade(id, newPlanId);
        }

        public object HasActive(int userId)
        {
            DataTable dt = _repo.HasActive(userId);

            if (dt.Rows.Count == 0)
                return new { hasActive = false, subscriptionId = 0 };

            return new
            {
                hasActive = true,
                subscriptionId = (int)dt.Rows[0]["SubscriptionId"]
            };
        }

        public object GetDetails(int id)
        {
            DataTable dt = _repo.GetDetails(id);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];

            return new
            {
                subscriptionId = (int)row["SubscriptionId"],
                userId = (int)row["UserId"],
                startDate = row["StartDate"],
                endDate = row["EndDate"],
                isActive = (bool)row["IsActive"],
                planName = row["PlanName"].ToString(),
                durationDays = (int)row["DurationDays"],
                price = (decimal)row["Price"]
            };
        }

        public void AutoDeactivateExpired()
        {
            _repo.AutoDeactivateExpired();
        }

        public List<object> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<object> list = new List<object>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new
                {
                    subscriptionId = (int)row["SubscriptionId"],
                    fullName = row["FullName"].ToString(),
                    planName = row["Name"].ToString(),
                    startDate = row["StartDate"],
                    endDate = row["EndDate"],
                    isActive = (bool)row["IsActive"]
                });
            }

            return list;
        }

        public int Deactivate(int id) => _repo.Deactivate(id);


    }
}
