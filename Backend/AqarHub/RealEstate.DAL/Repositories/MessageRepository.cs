using RealEstate.DAL.DataAccess;
using RealEstate.DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealEstate.DAL.Repositories
{
    public class MessageRepository
    {
        private readonly SqlHelper _sql;

        public MessageRepository(SqlHelper sql)
        {
            _sql = sql;
        }

        public int Add(Message msg)
        {
            string query = @"
                INSERT INTO Messages (SenderId, ReceiverId, PropertyId, Content, SentAt, IsRead)
                VALUES (@SenderId, @ReceiverId, @PropertyId, @Content, @SentAt, @IsRead)
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@SenderId", msg.SenderId),
                new SqlParameter("@ReceiverId", msg.ReceiverId),
                new SqlParameter("@PropertyId", (object?)msg.PropertyId ?? DBNull.Value),
                new SqlParameter("@Content", msg.Content),
                new SqlParameter("@SentAt", msg.SentAt),
                new SqlParameter("@IsRead", msg.IsRead)
            };

            return _sql.ExecuteNonQuery(query, parameters);
        }

        public List<Message> GetConversation(int u1, int u2)
        {
            string query = @"
                SELECT * FROM Messages
                WHERE (SenderId = @U1 AND ReceiverId = @U2)
                   OR (SenderId = @U2 AND ReceiverId = @U1)
                ORDER BY SentAt
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@U1", u1),
                new SqlParameter("@U2", u2)
            };

            var dt = _sql.ExecuteQuery(query, parameters);
            var list = new List<Message>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Message
                {
                    MessageId = (int)row["MessageId"],
                    SenderId = (int)row["SenderId"],
                    ReceiverId = (int)row["ReceiverId"],
                    PropertyId = row["PropertyId"] != DBNull.Value ? (int?)row["PropertyId"] : null,
                    Content = row["Content"].ToString(),
                    SentAt = Convert.ToDateTime(row["SentAt"]),
                    IsRead = Convert.ToBoolean(row["IsRead"])
                });
            }

            return list;
        }
    }
}
