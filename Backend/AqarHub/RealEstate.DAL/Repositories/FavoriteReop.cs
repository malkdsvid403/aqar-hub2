using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class FavoriteRepository
    {
        private readonly SqlHelper _sql;

        public FavoriteRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        public int Add(Favorite fav)
        {
            string query = @"
                INSERT INTO Favorites (UserId, PropertyId, CreatedAt)
                VALUES (@UserId, @PropertyId, @CreatedAt)
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", fav.UserId),
                new SqlParameter("@PropertyId", fav.PropertyId),
                new SqlParameter("@CreatedAt", fav.CreatedAt)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        public int Remove(int userId, int propertyId)
        {
            string query = @"
                DELETE FROM Favorites 
                WHERE UserId = @UserId AND PropertyId = @PropertyId
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@PropertyId", propertyId)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        public List<Favorite> GetByUser(int userId)
        {
            string query = "SELECT * FROM Favorites WHERE UserId = @UserId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId)
            };

            var dt = _sql.ExecuteQuery(query, parameters);
            var list = new List<Favorite>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                list.Add(new Favorite
                {
                    FavoriteId = (int)row["FavoriteId"],
                    UserId = (int)row["UserId"],
                    PropertyId = (int)row["PropertyId"],
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }

            return list;
        }
    }
}
