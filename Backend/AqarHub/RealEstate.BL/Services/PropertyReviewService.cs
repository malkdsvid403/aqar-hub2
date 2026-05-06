public class PropertyReviewService
{
    private readonly PropertyReviewRepository _repo;

    public PropertyReviewService(PropertyReviewRepository repo)
    {
        _repo = repo;
    }

    public int Add(PropertyReview review) => _repo.Add(review);

    public int Delete(int reviewId)
    {
        return _repo.Delete(reviewId);
    }

    public int Update(PropertyReview review)
    {
        return _repo.Update(review);
    }

    public int CountReviews(int propertyId)
    {
        return _repo.CountReviews(propertyId);
    }

    public double GetAverageRating(int propertyId)
    {
        return _repo.GetAverageRating(propertyId);
    }


    public List<PropertyReview> GetByProperty(int propertyId)
        => _repo.GetByProperty(propertyId);
}
