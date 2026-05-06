using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class DistrictRepository
    {
        private readonly SqlHelper _sql;

        public DistrictRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        public List<District> GetAll()
        {
            string query = "SELECT DistrictId, DistrictName, CityId FROM Districts";
            var dt = _sql.ExecuteQuery(query);

            var list = new List<District>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                list.Add(new District
                {
                    DistrictId = (int)row["DistrictId"],
                    DistrictName = row["DistrictName"].ToString(),
                    CityId = (int)row["CityId"]
                });
            }

            return list;
        }

        public int Add(District district)
        {
            string query = "INSERT INTO Districts (DistrictName, CityId) VALUES (@DistrictName, @CityId)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@DistrictName", district.DistrictName),
                new SqlParameter("@CityId", district.CityId)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }
    }
}
