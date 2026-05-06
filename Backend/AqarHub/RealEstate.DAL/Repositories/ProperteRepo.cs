using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class PropertyRepository
    {
        private readonly SqlHelper _sql;

        public PropertyRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        public List<Property> GetAll()
        {
            string query = "SELECT * FROM Properties";
            var dt = _sql.ExecuteQuery(query);

            var list = new List<Property>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }

            return list;
        }

        public int Add(Property p)
        {
            string query = @"
                INSERT INTO Properties 
                (Title, Description, Price, Area, Rooms, Bathrooms, Floor, Furnished, ListingType, Status,
                 PropertyTypeId, OwnerId, CityId, DistrictId, AddressText, Latitude, Longitude, IsFeatured, FeaturedUntil, CreatedAt)
                VALUES
                (@Title, @Description, @Price, @Area, @Rooms, @Bathrooms, @Floor, @Furnished, @ListingType, @Status,
                 @PropertyTypeId, @OwnerId, @CityId, @DistrictId, @AddressText, @Latitude, @Longitude, @IsFeatured, @FeaturedUntil, @CreatedAt)
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Title", p.Title),
                new SqlParameter("@Description", p.Description),
                new SqlParameter("@Price", p.Price),
                new SqlParameter("@Area", (object?)p.Area ?? DBNull.Value),
                new SqlParameter("@Rooms", (object?)p.Rooms ?? DBNull.Value),
                new SqlParameter("@Bathrooms", (object?)p.Bathrooms ?? DBNull.Value),
                new SqlParameter("@Floor", (object?)p.Floor ?? DBNull.Value),
                new SqlParameter("@Furnished", (object?)p.Furnished ?? DBNull.Value),
                new SqlParameter("@ListingType", p.ListingType),
                new SqlParameter("@Status", p.Status),
                new SqlParameter("@PropertyTypeId", p.PropertyTypeId),
                new SqlParameter("@OwnerId", p.OwnerId),
                new SqlParameter("@CityId", p.CityId),
                new SqlParameter("@DistrictId", p.DistrictId),
                new SqlParameter("@AddressText", p.AddressText),
                new SqlParameter("@Latitude", (object?)p.Latitude ?? DBNull.Value),
                new SqlParameter("@Longitude", (object?)p.Longitude ?? DBNull.Value),
                new SqlParameter("@IsFeatured", (object?)p.IsFeatured ?? DBNull.Value),
                new SqlParameter("@FeaturedUntil", (object?)p.FeaturedUntil ?? DBNull.Value),
                new SqlParameter("@CreatedAt", p.CreatedAt ?? DateTime.Now)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        public List<Property> GetByOwner(int ownerId)
        {
            string query = "SELECT * FROM Properties WHERE OwnerId = @OwnerId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@OwnerId", ownerId)
            };

            var dt = _sql.ExecuteQuery(query, parameters);
            var list = new List<Property>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }

            return list;
        }

        public int Delete(int id)
        {
            string query = "DELETE FROM Properties WHERE PropertyId = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", id)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        public int Update(Property property)
        {
            string query = @"
        UPDATE Properties
        SET 
            Title = @Title,
            Description = @Description,
            Price = @Price,
            CityId = @CityId,
            DistrictId = @DistrictId,
            PropertyTypeId = @PropertyTypeId,
            Area = @Area,
            Rooms = @Rooms,
            Bathrooms = @Bathrooms,
            Floor = @Floor,
            Furnished = @Furnished,
            ListingType = @ListingType,
            Status = @Status,
            AddressText = @AddressText,
            Latitude = @Latitude,
            Longitude = @Longitude,
            IsFeatured = @IsFeatured
        WHERE PropertyId = @PropertyId
    ";

            SqlParameter[] parameters =
            {
        new SqlParameter("@PropertyId", property.PropertyId),
        new SqlParameter("@Title", property.Title),
        new SqlParameter("@Description", property.Description),
        new SqlParameter("@Price", property.Price),
        new SqlParameter("@CityId", property.CityId),
        new SqlParameter("@DistrictId", property.DistrictId),
        new SqlParameter("@PropertyTypeId", property.PropertyTypeId),
        new SqlParameter("@Area", property.Area),
        new SqlParameter("@Rooms", property.Rooms),
        new SqlParameter("@Bathrooms", property.Bathrooms),
        new SqlParameter("@Floor", property.Floor),
        new SqlParameter("@Furnished", property.Furnished),
        new SqlParameter("@ListingType", property.ListingType),
        new SqlParameter("@Status", property.Status),
        new SqlParameter("@AddressText", property.AddressText),
        new SqlParameter("@Latitude", property.Latitude),
        new SqlParameter("@Longitude", property.Longitude),
        new SqlParameter("@IsFeatured", property.IsFeatured)
    };

            return _sql.ExecuteNonQuery(query, parameters);
        }


        private Property Map(System.Data.DataRow row)
        {
            return new Property
            {
                PropertyId = (int)row["PropertyId"],
                Title = row["Title"].ToString(),
                Description = row["Description"].ToString(),
                Price = Convert.ToDecimal(row["Price"]),
                Area = row["Area"] != DBNull.Value ? Convert.ToDouble(row["Area"]) : null,
                Rooms = row["Rooms"] != DBNull.Value ? Convert.ToInt32(row["Rooms"]) : null,
                Bathrooms = row["Bathrooms"] != DBNull.Value ? Convert.ToInt32(row["Bathrooms"]) : null,
                Floor = row["Floor"] != DBNull.Value ? Convert.ToInt32(row["Floor"]) : null,
                Furnished = row["Furnished"] != DBNull.Value ? Convert.ToBoolean(row["Furnished"]) : null,
                ListingType = row["ListingType"].ToString(),
                Status = row["Status"].ToString(),
                PropertyTypeId = (int)row["PropertyTypeId"],
                OwnerId = (int)row["OwnerId"],
                CityId = (int)row["CityId"],
                DistrictId = (int)row["DistrictId"],
                AddressText = row["AddressText"].ToString(),
                Latitude = row["Latitude"] != DBNull.Value ? Convert.ToDouble(row["Latitude"]) : null,
                Longitude = row["Longitude"] != DBNull.Value ? Convert.ToDouble(row["Longitude"]) : null,
                IsFeatured = row["IsFeatured"] != DBNull.Value ? Convert.ToBoolean(row["IsFeatured"]) : null,
                FeaturedUntil = row["FeaturedUntil"] != DBNull.Value ? Convert.ToDateTime(row["FeaturedUntil"]) : null,
                CreatedAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : null
            };
        }

        private List<Property> Map(DataTable dt)
        {
            var list = new List<Property>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row)); // ← استدعاء Map(DataRow)
            }

            return list;
        }


        public Property GetById(int id)
        {
            string query = "SELECT * FROM Properties WHERE PropertyId = @Id";

            SqlParameter[] parameters =
            {
        new SqlParameter("@Id", id)
    };

            var dt = _sql.ExecuteQuery(query, parameters);
            return Map(dt).FirstOrDefault();
        }

        public List<Property> Search(int? cityId, int? districtId, decimal? minPrice, decimal? maxPrice, int? typeId, int? rooms, bool? furnished)
        {
            string query = "SELECT * FROM Properties WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (cityId.HasValue)
            {
                query += " AND CityId = @CityId";
                parameters.Add(new SqlParameter("@CityId", cityId));
            }

            if (districtId.HasValue)
            {
                query += " AND DistrictId = @DistrictId";
                parameters.Add(new SqlParameter("@DistrictId", districtId));
            }

            if (minPrice.HasValue)
            {
                query += " AND Price >= @MinPrice";
                parameters.Add(new SqlParameter("@MinPrice", minPrice));
            }

            if (maxPrice.HasValue)
            {
                query += " AND Price <= @MaxPrice";
                parameters.Add(new SqlParameter("@MaxPrice", maxPrice));
            }

            if (typeId.HasValue)
            {
                query += " AND PropertyTypeId = @TypeId";
                parameters.Add(new SqlParameter("@TypeId", typeId));
            }

            if (rooms.HasValue)
            {
                query += " AND Rooms = @Rooms";
                parameters.Add(new SqlParameter("@Rooms", rooms));
            }

            if (furnished.HasValue)
            {
                query += " AND Furnished = @Furnished";
                parameters.Add(new SqlParameter("@Furnished", furnished));
            }

            var dt = _sql.ExecuteQuery(query, parameters.ToArray());
            return Map(dt);
        }

        public List<Property> GetFeatured()
        {
            string query = "SELECT * FROM Properties WHERE IsFeatured = 1";
            var dt = _sql.ExecuteQuery(query);
            return Map(dt);
        }


        public List<Property> GetPaged(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            string query = $@"
        SELECT * FROM Properties
        ORDER BY PropertyId DESC
        OFFSET {skip} ROWS
        FETCH NEXT {pageSize} ROWS ONLY
    ";

            var dt = _sql.ExecuteQuery(query);
            return Map(dt);
        }

        public List<Property> GetLatest(int count)
        {
            string query = $"SELECT TOP {count} * FROM Properties ORDER BY PropertyId DESC";
            var dt = _sql.ExecuteQuery(query);
            return Map(dt);
        }

    }
}
