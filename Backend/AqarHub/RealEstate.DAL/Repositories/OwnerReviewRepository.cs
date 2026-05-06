using RealEstate.DAL.DataAccess;
using System.Data.SqlClient;
using System.Data;

public class OwnerReviewRepository
{
    private readonly SqlHelper _sql;

    public OwnerReviewRepository(SqlHelper sql)
    {
        _sql = sql;
    }

    public int Add(OwnerReview review)
    {
        string query = @"
            INSERT INTO OwnerReviews (ReviewerId, OwnerId, Rating, Comment, CreatedAt)
            VALUES (@ReviewerId, @OwnerId, @Rating, @Comment, @CreatedAt)
        ";

        SqlParameter[] parameters =
        {
            new SqlParameter("@ReviewerId", review.ReviewerId),
            new SqlParameter("@OwnerId", review.OwnerId),
            new SqlParameter("@Rating", review.Rating),
            new SqlParameter("@Comment", (object?)review.Comment ?? DBNull.Value),
            new SqlParameter("@CreatedAt", review.CreatedAt)
        };

        return _sql.ExecuteNonQuery(query, parameters);
    }

    public List<OwnerReview> GetForOwner(int ownerId)
    {
        string query = "SELECT * FROM OwnerReviews WHERE OwnerId = @OwnerId";

        SqlParameter[] parameters =
        {
            new SqlParameter("@OwnerId", ownerId)
        };

        var dt = _sql.ExecuteQuery(query, parameters);
        var list = new List<OwnerReview>();

        foreach (DataRow row in dt.Rows)
        {
            list.Add(new OwnerReview
            {
                ReviewId = (int)row["ReviewId"],
                ReviewerId = (int)row["ReviewerId"],
                OwnerId = (int)row["OwnerId"],
                Rating = (int)row["Rating"],
                Comment = row["Comment"]?.ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            });
        }

        return list;
    }
}
