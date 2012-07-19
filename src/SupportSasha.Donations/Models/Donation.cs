using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportSasha.Donations.Models
{

    public class TempMessage
    {
        private readonly string _type;
        public string Type
        {
            get { return _type; }
        }
        private readonly string _message;
        public string Message
        {
            get { return _message; }
        }
        /// <summary>
        /// Initializes a new instance of the TempMessage class.
        /// </summary>
        public TempMessage(string message)
            : this(message, "alert")
        {
        }

        /// <summary>
        /// Initializes a new instance of the TempMessage class.
        /// </summary>
        public TempMessage(string message, string type)
        {
            _message = message;
            _type = type;            
        }




    }
    public class Donation
    {
        public string Id { get; set; }
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