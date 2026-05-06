using RealEstate.DAL.DataAccess;
using System.Data.SqlClient;
using System.Data;

public class PropertyReviewRepository
{
    private readonly SqlHelper _sql;

    public PropertyReviewRepository(SqlHelper sql)
    {
        _sql = sql;
    }

    public int Add(PropertyReview review)
    {
        string query = @"
            INSERT INTO PropertyReviews (UserId, PropertyId, Rating, Comment, CreatedAt)
            VALUES (@UserId, @PropertyId, @Rating, @Comment, @CreatedAt)
        ";

        SqlParameter[] parameters =
        {
            new SqlParameter("@UserId", review.UserId),
            new SqlParameter("@PropertyId", review.PropertyId),
            new SqlParameter("@Rating", review.Rating),
            new SqlParameter("@Comment", (object?)review.Comment ?? DBNull.Value),
            new SqlParameter("@CreatedAt", DateTime.Now)
        };

        return _sql.ExecuteNonQuery(query, parameters);
    }

    public int Delete(int reviewId)
    {
        string query = "DELETE FROM PropertyReviews WHERE ReviewId = @ReviewId";

        SqlParameter[] parameters =
        {
        new SqlParameter("@ReviewId", reviewId)
    };

        return _sql.ExecuteNonQuery(query, parameters);
    }


    public int Update(PropertyReview review)
    {
        string query = @"
        UPDATE PropertyReviews
        SET Rating = @Rating,
            Comment = @Comment
        WHERE ReviewId = @ReviewId
    ";

        SqlParameter[] parameters =
        {
        new SqlParameter("@ReviewId", review.ReviewId),
        new SqlParameter("@Rating", review.Rating),
        new SqlParameter("@Comment", review.Comment)
    };

        return _sql.ExecuteNonQuery(query, parameters);
    }

    public int CountReviews(int propertyId)
    {
        string query = "SELECT COUNT(*) FROM PropertyReviews WHERE PropertyId = @PropertyId";

        SqlParameter[] parameters =
        {
        new SqlParameter("@PropertyId", propertyId)
    };

        var result = _sql.ExecuteScalar(query, parameters);
        return Convert.ToInt32(result);
    }

    public double GetAverageRating(int propertyId)
    {
        string query = "SELECT AVG(Rating) FROM PropertyReviews WHERE PropertyId = @PropertyId";

        SqlParameter[] parameters =
        {
        new SqlParameter("@PropertyId", propertyId)
    };

        var result = _sql.ExecuteScalar(query, parameters);

        return result != DBNull.Value ? Convert.ToDouble(result) : 0;
    }


    public List<PropertyReview> GetByProperty(int propertyId)
    {
        string query = "SELECT * FROM PropertyReviews WHERE PropertyId = @PropertyId";

        SqlParameter[] parameters =
        {
            new SqlParameter("@PropertyId", propertyId)
        };

        var dt = _sql.ExecuteQuery(query, parameters);
        var list = new List<PropertyReview>();

        foreach (DataRow row in dt.Rows)
        {
            list.Add(new PropertyReview
            {
                ReviewId = (int)row["ReviewId"],
                UserId = (int)row["UserId"],
                PropertyId = (int)row["PropertyId"],
                Rating = (int)row["Rating"],
                Comment = row["Comment"]?.ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            });
        }

        return list;
    }
}
