using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportSasha.Donations.Models
{
    public class SwimProgressModel
    {
        public int PurchasedSwimLanes { get; set; }
        public double ProgressPercentage { get { return ((double)PurchasedSwimLanes / (double)TotalSwimLanes) * 100.00; } }
        public IEnumerable<Donation> Donations { get; set; }

        public int TotalSwimLanes { get { return 1000; } }
    }
}