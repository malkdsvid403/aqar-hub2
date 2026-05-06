namespace RealEstate.DAL.Models
{
    public class Property
    {
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double? Area { get; set; }
        public int? Rooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Floor { get; set; }
        public bool? Furnished { get; set; }
        public string ListingType { get; set; }
        public string Status { get; set; }
        public int PropertyTypeId { get; set; }
        public int OwnerId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string AddressText { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool? IsFeatured { get; set; }
        public DateTime? FeaturedUntil { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
