using System;
using System.Collections.Generic;

namespace E_Commerce.DAL.Models
{
    public partial class ProductMaster
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
    }
}
