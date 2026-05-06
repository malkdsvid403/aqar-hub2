public class OwnerReviewService
{
    private readonly OwnerReviewRepository _repo;

    public OwnerReviewService(OwnerReviewRepository repo)
    {
        _repo = repo;
    }

    public int Add(OwnerReview review) => _repo.Add(review);

    public List<OwnerReview> GetForOwner(int ownerId)
        => _repo.GetForOwner(ownerId);
}
