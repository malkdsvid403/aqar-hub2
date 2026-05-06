namespace RealEstate.DAL.Models
{
    public class Plan
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public int? MaxProperties { get; set; }
        public int? MaxFeatured { get; set; }
        public string Description { get; set; }
    }
}
