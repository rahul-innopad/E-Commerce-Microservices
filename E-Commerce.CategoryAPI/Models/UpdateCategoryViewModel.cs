namespace E_Commerce.CategoryAPI.Models
{
    public class UpdateCategoryViewModel
    {
        public long Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? UniqueCategiryCode { get; set; }
        public string UniqueCategoryId { get; set; } = null!;
    }
}
