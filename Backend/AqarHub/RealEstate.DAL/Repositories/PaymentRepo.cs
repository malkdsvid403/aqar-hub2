using RealEstate.DAL.DataAccess;
using System.Data.SqlClient;
using System.Data;

public class PaymentRepository
{
    private readonly SqlHelper _sql;

    public PaymentRepository(SqlHelper sql)
    {
        _sql = sql;
    }

    public int Add(Payment payment)
    {
        string query = @"
            INSERT INTO Payments (UserId, PlanId, Amount, PaymentMethod, Status, PaymentDate, TransactionId)
            VALUES (@UserId, @PlanId, @Amount, @PaymentMethod, @Status, @PaymentDate, @TransactionId)
        ";

        SqlParameter[] parameters =
        {
            new SqlParameter("@UserId", payment.UserId),
            new SqlParameter("@PlanId", payment.PlanId),
            new SqlParameter("@Amount", payment.Amount),
            new SqlParameter("@PaymentMethod", payment.PaymentMethod),
            new SqlParameter("@Status", payment.Status),
            new SqlParameter("@PaymentDate", payment.PaymentDate),
            new SqlParameter("@TransactionId", payment.TransactionId)
        };

        return _sql.ExecuteNonQuery(query, parameters);
    }

    public List<Payment> GetAll()
    {
        string query = "SELECT * FROM Payments";

        var dt = _sql.ExecuteQuery(query);
        var list = new List<Payment>();

        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Payment
            {
                PaymentId = (int)row["PaymentId"],
                UserId = (int)row["UserId"],
                PlanId = (int)row["PlanId"],
                Amount = Convert.ToDecimal(row["Amount"]),
                PaymentMethod = row["PaymentMethod"].ToString(),
                Status = row["Status"].ToString(),
                PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                TransactionId = row["TransactionId"].ToString()
            });
        }

        return list;
    }

    public List<Payment> GetByUser(int userId)
    {
        string query = "SELECT * FROM Payments WHERE UserId = @UserId";

        SqlParameter[] parameters =
        {
        new SqlParameter("@UserId", userId)
    };

        var dt = _sql.ExecuteQuery(query, parameters);
        var list = new List<Payment>();

        Console.WriteLine("Columns:");
        foreach (DataColumn col in dt.Columns)
        {
            Console.WriteLine(col.ColumnName);
        }


        /*foreach (DataRow row in dt.Rows)
        {
            list.Add(new Payment
            {
                PaymentId = (int)row["PaymentId"],
                UserId = (int)row["UserId"],
                PlanId = (int)row["PlanId"],
                Amount = Convert.ToDecimal(row["Amount"]),
                PaymentMethod = row["PaymentMethod"].ToString(),
                Status = row["Status"].ToString(),
                PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                TransactionId = row["TransactionId"].ToString()
            });
        }
        */
        return list;
    }

}
