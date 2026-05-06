using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class UserRepository
    {
        private readonly SqlHelper _sql;

        public UserRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        public List<User> GetAll()
        {
            string query = "SELECT UserId, FirstName, LastName, Email, Password, Phone, RoleId, CityId, DistrictId FROM Users";
            var dt = _sql.ExecuteQuery(query);

            var list = new List<User>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                list.Add(new User
                {
                    UserId = (int)row["UserId"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString(),
                    Phone = row["Phone"].ToString(),
                    RoleId = (int)row["RoleId"],
                    CityId = (int)row["CityId"],
                    DistrictId = (int)row["DistrictId"]
                });
            }

            return list;
        }

        public int Add(User user)
        {
            string query = @"INSERT INTO Users 
                            (FirstName, LastName, Email, Password, Phone, RoleId, CityId, DistrictId)
                             VALUES (@FirstName, @LastName, @Email, @Password, @Phone, @RoleId, @CityId, @DistrictId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Phone", user.Phone),
                new SqlParameter("@RoleId", user.RoleId),
                new SqlParameter("@CityId", user.CityId),
                new SqlParameter("@DistrictId", user.DistrictId)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        public User Login(string email, string password)
        {
            string query = @"SELECT * FROM Users WHERE Email = @Email AND Password = @Password";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };

            var dt = _sql.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];

            return new User
            {
                UserId = (int)row["UserId"],
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Email = row["Email"].ToString(),
                Password = row["Password"].ToString(),
                Phone = row["Phone"].ToString(),
                RoleId = (int)row["RoleId"],
                CityId = (int)row["CityId"],
                DistrictId = (int)row["DistrictId"]
            };
        }

        public User GetById(int id)
        {
            string query = "SELECT * FROM Users WHERE UserId = @UserId";

            SqlParameter[] parameters =
            {
        new SqlParameter("@UserId", id)
    };

            DataTable dt = _sql.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return new User
            {
                UserId = (int)row["UserId"],
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString(),
                Password = row["Password"].ToString(),
                RoleId = (int)row["RoleId"],
                CityId = (int)row["CityId"],
                DistrictId = (int)row["DistrictId"]
            };
        }

        public int ChangePassword(int userId, string newPassword)
        {
            string query = "UPDATE Users SET Password = @Password WHERE UserId = @UserId";

            SqlParameter[] parameters =
            {
        new SqlParameter("@UserId", userId),
        new SqlParameter("@Password", newPassword)
    };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        public List<User> GetUsersByRole(int roleId)
        {
            string query = "SELECT * FROM Users WHERE RoleId = @RoleId";

            SqlParameter[] parameters =
            {
        new SqlParameter("@RoleId", roleId)
    };

            DataTable dt = _sql.ExecuteQuery(query, parameters);
            List<User> list = new List<User>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new User
                {
                    UserId = (int)row["UserId"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Password = row["Password"].ToString(),
                    RoleId = (int)row["RoleId"],
                    CityId = (int)row["CityId"],
                    DistrictId = (int)row["DistrictId"]
                });
            }

            return list;
        }

        public UserProfileDto GetUserProfile(int userId)
        {
            string query = @"
        SELECT 
            u.UserId,
            u.FirstName + ' ' + u.LastName AS FullName,
            u.Email,
            u.Phone,
            r.RoleName,
            c.CityName,
            d.DistrictName
        FROM Users u
        JOIN Roles r ON u.RoleId = r.RoleId
        JOIN Cities c ON u.CityId = c.CityId
        JOIN Districts d ON u.DistrictId = d.DistrictId
        WHERE u.UserId = @UserId
    ";

            SqlParameter[] parameters =
            {
        new SqlParameter("@UserId", userId)
    };

            DataTable dt = _sql.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return new UserProfileDto
            {
                UserId = (int)row["UserId"],
                FullName = row["FullName"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString(),
                RoleName = row["RoleName"].ToString(),
                CityName = row["CityName"].ToString(),
                DistrictName = row["DistrictName"].ToString()
            };
        }


        public List<User> Search(string keyword)
        {
            string query = @"
        SELECT * FROM Users
        WHERE FirstName LIKE @k
           OR LastName LIKE @k
           OR Email LIKE @k
           OR Phone LIKE @k";

            SqlParameter[] parameters =
            {
        new SqlParameter("@k", "%" + keyword + "%")
    };

            DataTable dt = _sql.ExecuteQuery(query, parameters);
            List<User> list = new List<User>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new User
                {
                    UserId = (int)row["UserId"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Password = row["Password"].ToString(),
                    RoleId = (int)row["RoleId"],
                    CityId = (int)row["CityId"],
                    DistrictId = (int)row["DistrictId"]
                });
            }

            return list;
        }

        public int Update(User user)
        {
            string query = @"
        UPDATE Users
        SET 
            FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            Phone = @Phone,
            RoleId = @RoleId,
            CityId = @CityId,
            DistrictId = @DistrictId
        WHERE UserId = @UserId
    ";

            SqlParameter[] parameters =
            {
        new SqlParameter("@UserId", user.UserId),
        new SqlParameter("@FirstName", user.FirstName),
        new SqlParameter("@LastName", user.LastName),
        new SqlParameter("@Email", user.Email),
        new SqlParameter("@Phone", user.Phone),
        new SqlParameter("@RoleId", user.RoleId),
        new SqlParameter("@CityId", user.CityId),
        new SqlParameter("@DistrictId", user.DistrictId)
    };

            return _sql.ExecuteNonQuery(query, parameters);
        }


        public int Delete(int userId)
        {
            string query = "DELETE FROM Users WHERE UserId = @UserId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }
    }
}
