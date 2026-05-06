using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class PropertyImageRepository
    {
        private readonly SqlHelper _sql;

        public PropertyImageRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        // Get all images for a property
        public List<PropertyImage> GetByProperty(int propertyId)
        {
            string query = "SELECT * FROM PropertyImages WHERE PropertyId = @PropertyId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@PropertyId", propertyId)
            };

            var dt = _sql.ExecuteQuery(query, parameters);
            var list = new List<PropertyImage>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                list.Add(new PropertyImage
                {
                    ImageId = (int)row["ImageId"],
                    PropertyId = (int)row["PropertyId"],
                    ImageUrl = row["ImageUrl"].ToString(),
                    IsMain = (bool)row["IsMain"],
                    CreatedAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : null
                });
            }

            return list;
        }

        // Add new image
        public int Add(PropertyImage img)
        {
            string query = @"
                INSERT INTO PropertyImages (PropertyId, ImageUrl, IsMain)
                VALUES (@PropertyId, @ImageUrl, @IsMain)
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@PropertyId", img.PropertyId),
                new SqlParameter("@ImageUrl", img.ImageUrl),
                new SqlParameter("@IsMain", img.IsMain)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        // Delete image
        public int Delete(int imageId)
        {
            string query = "DELETE FROM PropertyImages WHERE ImageId = @ImageId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ImageId", imageId)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }
    }
}
