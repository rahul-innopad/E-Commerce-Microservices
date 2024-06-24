namespace E_Commerce.ProductAPI.Models
{
    public class GetProductViewModel
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public bool? IsAvailable { get; set; }
        public double? Price { get; set; }
        public double? DiscountedPrice { get; set; }
        public string? UniqueProductId { get; set; }
        public string? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class GetProductViewModelList
    {
        public long Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? Sku { get; set; }
        public bool? IsAvailable { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public double? Price { get; set; }
        public double? DiscountedPrice { get; set; }
        public string? UniqueProductId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CategoryId { get; set; }
    }
}

