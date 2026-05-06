using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class PropertyTypeRepository
    {
        private readonly SqlHelper _sql;

        public PropertyTypeRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        public List<PropertyType> GetAll()
        {
            string query = "SELECT PropertyTypeId, TypeName FROM PropertyTypes";
            var dt = _sql.ExecuteQuery(query);

            var list = new List<PropertyType>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                list.Add(new PropertyType
                {
                    PropertyTypeId = (int)row["PropertyTypeId"],
                    TypeName = row["TypeName"].ToString()
                });
            }

            return list;
        }

        public int Update(PropertyType type)
        {
            string query = @"
        UPDATE PropertyTypes
        SET TypeName = @TypeName
        WHERE PropertyTypeId = @PropertyTypeId
    ";

            SqlParameter[] parameters =
            {
        new SqlParameter("@PropertyTypeId", type.PropertyTypeId),
        new SqlParameter("@TypeName", type.TypeName)
    };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        private PropertyType Map(DataRow row)
        {
            return new PropertyType
            {
                PropertyTypeId = (int)row["PropertyTypeId"],
                TypeName = row["TypeName"].ToString()
            };
        }

        private List<PropertyType> Map(DataTable dt)
        {
            var list = new List<PropertyType>();
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }


        public List<PropertyType> Search(string name)
        {
            string query = "SELECT PropertyTypeId, TypeName FROM PropertyTypes WHERE TypeName LIKE @Name";

            SqlParameter[] parameters =
            {
        new SqlParameter("@Name", "%" + name + "%")
    };

            var dt = _sql.ExecuteQuery(query, parameters);
            return Map(dt);
        }


        public int CountPropertiesByType(int typeId)
        {
            string query = "SELECT COUNT(*) FROM Properties WHERE PropertyTypeId = @TypeId";

            SqlParameter[] parameters =
            {
        new SqlParameter("@TypeId", typeId)
    };

            var result = _sql.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result);
        }

        public int Add(PropertyType type)
        {
            string query = "INSERT INTO PropertyTypes (TypeName) VALUES (@TypeName)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeName", type.TypeName)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }
    }
}
