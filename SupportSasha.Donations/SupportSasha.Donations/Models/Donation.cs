using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportSasha.Donations.Models
{
    public class Donation
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Provider { get; set; }    
    }
}
