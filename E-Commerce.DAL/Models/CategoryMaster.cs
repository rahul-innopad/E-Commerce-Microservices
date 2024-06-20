using System;
using System.Collections.Generic;

namespace E_Commerce.DAL.Models
{
    public partial class CategoryMaster
    {
        public long Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdateAt { get; set; }
        public string? UniqueCategiryCode { get; set; }
    }
}
