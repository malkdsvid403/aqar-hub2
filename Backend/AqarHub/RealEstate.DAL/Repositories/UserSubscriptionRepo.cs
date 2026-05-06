using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class UserSubscriptionRepository
    {
        private readonly SqlHelper _sql;

        public UserSubscriptionRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        public List<UserSubscription> GetByUser(int userId)
        {
            string query = "SELECT * FROM UserSubscriptions WHERE UserId = @UserId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId)
            };

            var dt = _sql.ExecuteQuery(query, parameters);
            var list = new List<UserSubscription>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                list.Add(new UserSubscription
                {
                    SubscriptionId = (int)row["SubscriptionId"],
                    UserId = (int)row["UserId"],
                    PlanId = (int)row["PlanId"],
                    StartDate = Convert.ToDateTime(row["StartDate"]),
                    EndDate = Convert.ToDateTime(row["EndDate"]),
                    RemainingProperties = row["RemainingProperties"] != DBNull.Value ? (int?)row["RemainingProperties"] : null,
                    RemainingFeatured = row["RemainingFeatured"] != DBNull.Value ? (int?)row["RemainingFeatured"] : null
                });
            }

            return list;
        }

        public int Add(UserSubscription sub)
        {
            string query = @"
                INSERT INTO UserSubscriptions 
                (UserId, PlanId, StartDate, EndDate, RemainingProperties, RemainingFeatured)
                VALUES (@UserId, @PlanId, @StartDate, @EndDate, @RemainingProperties, @RemainingFeatured)
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", sub.UserId),
                new SqlParameter("@PlanId", sub.PlanId),
                new SqlParameter("@StartDate", sub.StartDate),
                new SqlParameter("@EndDate", sub.EndDate),
                new SqlParameter("@RemainingProperties", (object?)sub.RemainingProperties ?? DBNull.Value),
                new SqlParameter("@RemainingFeatured", (object?)sub.RemainingFeatured ?? DBNull.Value)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        public UserSubscription GetActive(int userId)
        {
            string query = "SELECT TOP 1 * FROM UserSubscriptions WHERE UserId = @UserId AND IsActive = 1 ORDER BY EndDate DESC";

            SqlParameter[] parameters =
            {
        new SqlParameter("@UserId", userId)
    };

            var dt = _sql.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];

            return new UserSubscription
            {
                SubscriptionId = (int)row["SubscriptionId"],
                UserId = (int)row["UserId"],
                PlanId = (int)row["PlanId"],
                StartDate = Convert.ToDateTime(row["StartDate"]),
                EndDate = Convert.ToDateTime(row["EndDate"]),
                IsActive = Convert.ToBoolean(row["IsActive"])
            };
        }

        public int Delete(int id)
        {
            string query = "DELETE FROM UserSubscriptions WHERE SubscriptionId = @Id";

            SqlParameter[] parameters =
            {
        new SqlParameter("@Id", id)
    };

            return _sql.ExecuteNonQuery(query, parameters);
        }


        // 1) Extend subscription
        public int Extend(int id, int days)
        {
            string query = @"
                UPDATE UserSubscriptions
                SET EndDate = DATEADD(day, @days, EndDate)
                WHERE SubscriptionId = @id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@id", id),
                new SqlParameter("@days", days)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        // 2) Upgrade subscription
        public int Upgrade(int id, int newPlanId)
        {
            string query = @"
                UPDATE UserSubscriptions
                SET PlanId = @newPlanId
                WHERE SubscriptionId = @id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@id", id),
                new SqlParameter("@newPlanId", newPlanId)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        // 3) Check if user has active subscription
        public DataTable HasActive(int userId)
        {
            string query = @"
                SELECT TOP 1 SubscriptionId
                FROM UserSubscriptions
                WHERE UserId = @userId AND IsActive = 1
                ORDER BY EndDate DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", userId)
            };

            return _sql.ExecuteQuery(query, parameters);
        }

        // 4) Get subscription details (with plan info)
        public DataTable GetDetails(int id)
        {
            string query = @"
SELECT 
    us.SubscriptionId,
    us.UserId,
    us.StartDate,
    us.EndDate,
    us.IsActive,
    p.Name AS PlanName,
    p.DurationDays,
    p.Price
FROM UserSubscriptions us
JOIN Plans p ON us.PlanId = p.PlanId
WHERE us.SubscriptionId = @id

";

            SqlParameter[] parameters =
            {
                new SqlParameter("@id", id)
            };

            return _sql.ExecuteQuery(query, parameters);
        }

        // 5) Auto deactivate expired subscriptions
        public int AutoDeactivateExpired()
        {
            string query = @"
                UPDATE UserSubscriptions
                SET IsActive = 0
                WHERE EndDate < GETDATE() AND IsActive = 1";

            return _sql.ExecuteNonQuery(query);
        }

        // 6) Get all subscriptions (Admin)
        public DataTable GetAll()
        {
            string query = @"
                SELECT 
                    us.SubscriptionId,
                    u.FirstName + ' ' + u.LastName AS FullName,
                    p.Name,
                    us.StartDate,
                    us.EndDate,
                    us.IsActive
                FROM UserSubscriptions us
                JOIN Users u ON us.UserId = u.UserId
                JOIN Plans p ON us.PlanId = p.PlanId";

            return _sql.ExecuteQuery(query);
        }
        public int Deactivate(int subscriptionId)
        {
            string query = @"
        UPDATE UserSubscriptions
        SET IsActive = 0
        WHERE SubscriptionId = @Id
    ";

            SqlParameter[] parameters =
            {
        new SqlParameter("@Id", subscriptionId)
    };

            return _sql.ExecuteNonQuery(query, parameters);
        }


    }
}
