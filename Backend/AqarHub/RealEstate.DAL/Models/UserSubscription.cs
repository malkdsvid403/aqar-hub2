public class UserSubscription
{
    public int SubscriptionId { get; set; }
    public int UserId { get; set; }
    public int PlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public int? RemainingProperties { get; set; }
    public int? RemainingFeatured { get; set; }
}
