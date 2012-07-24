using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SupportSasha.Donations.Models
{
    public class DonationInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        
        public decimal Amount { get; set; }
        public decimal? OtherAmount { get; set; }

        public string Message { get; set; }
        public bool DontShowName { get; set; }
        public string Campaign { get; set; }
    }
}
