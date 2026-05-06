using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class RoleRepository
    {
        private readonly SqlHelper _sql;

        public RoleRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        // ============================
        // 1) Get All
        // ============================
        public List<Role> GetAll()
        {
            string query = "SELECT RoleId, RoleName FROM Roles";
            var dt = _sql.ExecuteQuery(query);
            return Map(dt);
        }

        // ============================
        // 2) Get By Id
        // ============================
        public Role GetById(int roleId)
        {
            string query = "SELECT RoleId, RoleName FROM Roles WHERE RoleId = @RoleId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleId", roleId)
            };

            var dt = _sql.ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0 ? Map(dt.Rows[0]) : null;
        }

        // ============================
        // 3) Exists
        // ============================
        public bool Exists(string roleName)
        {
            string query = "SELECT COUNT(*) FROM Roles WHERE RoleName = @RoleName";

            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleName", roleName)
            };

            return Convert.ToInt32(_sql.ExecuteScalar(query, parameters)) > 0;
        }

        // ============================
        // 4) Has Users
        // ============================
        public bool HasUsers(int roleId)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE RoleId = @RoleId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleId", roleId)
            };

            return Convert.ToInt32(_sql.ExecuteScalar(query, parameters)) > 0;
        }

        // ============================
        // 5) Count Users In Role
        // ============================
        public int CountUsersInRole(int roleId)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE RoleId = @RoleId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleId", roleId)
            };

            return Convert.ToInt32(_sql.ExecuteScalar(query, parameters));
        }

        // ============================
        // 6) Add
        // ============================
        public int Add(Role role)
        {
            if (Exists(role.RoleName))
                throw new Exception("Role already exists");

            string query = "INSERT INTO Roles (RoleName) VALUES (@RoleName)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleName", role.RoleName)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        // ============================
        // 7) Update
        // ============================
        public int Update(Role role)
        {
            string query = @"
                UPDATE Roles
                SET RoleName = @RoleName
                WHERE RoleId = @RoleId
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleId", role.RoleId),
                new SqlParameter("@RoleName", role.RoleName)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        // ============================
        // 8) Delete
        // ============================
        public int Delete(int roleId)
        {
            if (HasUsers(roleId))
                throw new Exception("Cannot delete role because users are assigned to it");

            string query = "DELETE FROM Roles WHERE RoleId = @RoleId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleId", roleId)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        // ============================
        // 9) Search
        // ============================
        public List<Role> Search(string name)
        {
            string query = "SELECT RoleId, RoleName FROM Roles WHERE RoleName LIKE @Name";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", "%" + name + "%")
            };

            var dt = _sql.ExecuteQuery(query, parameters);
            return Map(dt);
        }

        // ============================
        // 10) Roles With User Count
        // ============================
        public List<RoleWithCount> GetRolesWithUserCount()
        {
            string query = @"
                SELECT r.RoleId, r.RoleName, COUNT(u.UserId) AS UserCount
                FROM Roles r
                LEFT JOIN Users u ON r.RoleId = u.RoleId
                GROUP BY r.RoleId, r.RoleName
                ORDER BY UserCount DESC
            ";

            var dt = _sql.ExecuteQuery(query);

            var list = new List<RoleWithCount>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new RoleWithCount
                {
                    RoleId = (int)row["RoleId"],
                    RoleName = row["RoleName"].ToString(),
                    UserCount = Convert.ToInt32(row["UserCount"])
                });
            }

            return list;
        }

        // ============================
        // Mapping
        // ============================
        private Role Map(DataRow row)
        {
            return new Role
            {
                RoleId = (int)row["RoleId"],
                RoleName = row["RoleName"].ToString()
            };
        }

        private List<Role> Map(DataTable dt)
        {
            var list = new List<Role>();
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }
    }
}
