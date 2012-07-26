using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportSasha.Donations.Models
{
    public class DomainEntity
    {
        public string Id { get; set; }
        public string ShortId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Id))
                    return null;
                if (Id.Contains('/'))
                    return Id.Split('/')[1];
                else
                    return Id;
            }
        }
    }
    public class Target : DomainEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string MoreInfoLink { get; set; }
    }
}
