using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportSasha.Donations.Models
{
    public class DonationInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
        public bool DontShowName { get; set; }
        public string Campaign { get; set; }
    }
}
