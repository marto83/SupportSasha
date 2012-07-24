using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace SupportSasha.Donations.Models
{

    public class Donation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        private string _campaignName;
        public string CampaignName
        {
            get { return _campaignName ?? "General"; }
            set { _campaignName = value; }
        }
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool Confirmed { get; set; }
        public bool DontShowName { get; set; }

        public string GetPublicName()
        {
            return DontShowName ? "Anonymous" : Name;
        }

        /// <summary>
        /// Initializes a new instance of the Donation class.
        /// </summary>
        public Donation()
        {
            Date = DateTimeOffset.Now;
        }

        public static Donation CreatFromInput(DonationInput attempt)
        {
            return new Donation
            {
                Name = attempt.Name,
                Amount = attempt.Amount,
                Message = attempt.Message,
                Email = attempt.Email,
                DontShowName = attempt.DontShowName,
                Date = DateTimeOffset.Now,
                CampaignName = attempt.Campaign ?? "General"
            };
        }
    }
}