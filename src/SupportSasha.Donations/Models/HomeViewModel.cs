using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportSasha.Donations.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Target> Targets { get; set; }
        public IEnumerable<Donation> Donations { get; set; }
        public decimal TargetsTotal { get; set; }
        public double ProgressPercentage { get; set; }
    }
}