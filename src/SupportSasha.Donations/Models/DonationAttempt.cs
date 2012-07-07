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
        public string CampaignName { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public bool Confirmed { get; set; }
    }
}