using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class CityRepository
    {
        private readonly SqlHelper _sql;

        public CityRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        public List<City> GetAll()
        {
            string query = "SELECT * FROM Cities";
            var dt = _sql.ExecuteQuery(query);

            var list = new List<City>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                list.Add(new City
                {
                    CityId = (int)row["CityId"],
                    CityName = row["CityName"].ToString()
                });
            }

            return list;
        }

        public int Add(City city)
        {
            string query = "INSERT INTO Cities (CityName) VALUES (@CityName)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@CityName", city.CityName)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }
    }
}
