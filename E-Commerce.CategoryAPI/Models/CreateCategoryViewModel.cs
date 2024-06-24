namespace E_Commerce.CategoryAPI.Models
{
    public class CreateCategoryViewModel
    {
        public long Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? UniqueCategiryCode { get; set; }
    }
}
