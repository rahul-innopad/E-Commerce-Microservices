using System;
using System.Collections.Generic;

namespace E_Commerce.DAL.Models
{
    public partial class Register33
    {
        public int FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
        public int? IsActive { get; set; }
        public int? RegisterId { get; set; }
    }
}
