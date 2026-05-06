using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class PlanRepository
    {
        private readonly SqlHelper _sql;

        public PlanRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        public List<Plan> GetAll()
        {
            string query = "SELECT * FROM Plans";

            var dt = _sql.ExecuteQuery(query);
            var list = new List<Plan>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Plan
                {
                    PlanId = (int)row["PlanId"],
                    Name = row["Name"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    MaxProperties = (int)row["MaxProperties"],
                    MaxFeatured = (int)row["MaxFeatured"],
                    Description = row["Description"].ToString()
                });
            }

            return list;
        }


        public int Add(Plan plan)
        {
            string query = @"
        INSERT INTO Plans (Name, Price, MaxProperties, MaxFeatured, Description)
        VALUES (@Name, @Price, @MaxProperties, @MaxFeatured, @Description)
    ";

            SqlParameter[] parameters =
            {
        new SqlParameter("@Name", plan.Name),
        new SqlParameter("@Price", plan.Price),
        new SqlParameter("@MaxProperties", plan.MaxProperties),
        new SqlParameter("@MaxFeatured", plan.MaxFeatured),
        new SqlParameter("@Description", plan.Description)
    };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        public int Delete(int planId)
        {
            string query = "DELETE FROM Plans WHERE PlanId = @PlanId";

            SqlParameter[] parameters =
            {
        new SqlParameter("@PlanId", planId)
    };

            return _sql.ExecuteNonQuery(query, parameters);
        }


        public int Update(Plan plan)
        {
            string query = @"
        UPDATE Plans
        SET Name = @Name,
            Price = @Price,
            MaxProperties = @MaxProperties,
            MaxFeatured = @MaxFeatured,
            Description = @Description
        WHERE PlanId = @PlanId
    ";

            SqlParameter[] parameters =
            {
        new SqlParameter("@PlanId", plan.PlanId),
        new SqlParameter("@Name", plan.Name),
        new SqlParameter("@Price", plan.Price),
        new SqlParameter("@MaxProperties", plan.MaxProperties),
        new SqlParameter("@MaxFeatured", plan.MaxFeatured),
        new SqlParameter("@Description", plan.Description)
    };

            return _sql.ExecuteNonQuery(query, parameters);
        }

    }
}
