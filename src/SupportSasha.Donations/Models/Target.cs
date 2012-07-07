using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportSasha.Donations.Models
{
    public class Target
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
