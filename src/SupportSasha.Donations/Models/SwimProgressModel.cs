using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportSasha.Donations.Models
{
    public class EventProgressModel
    {
        public int CurrentAmount { get; set; }
        public double ProgressPercentage { get { return ((double)CurrentAmount / (double)Target) * 100.00; } }
        public IEnumerable<Donation> Donations { get; set; }

        public int Target { get; set; }
    }
}