using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportSasha.Donations.Models
{
    public class DonationAttempt
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string Provider { get; set; }
    }
}