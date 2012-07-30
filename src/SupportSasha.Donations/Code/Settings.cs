using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SupportSasha.Donations.Code
{
    public static class Settings
    {
        public static class Website
        {
            public static string ThankyouEmailUrl
            {
                get { return ConfigurationManager.AppSettings["Website:ThankyouEmailUrl"]; }
            }
        }
    }
}