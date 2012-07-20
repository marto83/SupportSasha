using System;
using System.Collections.Generic;
using System.Linq;

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
}
