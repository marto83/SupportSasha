using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SupportSasha.Donations.Code
{
    public static class PaypalSettings
    {
        public static bool IsSandbox
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["Paypal:Sandbox"]); }
        }   

        public static string PaypalUrl
        {
            get { return IsSandbox ? "https://www.sandbox.paypal.com" : "https://www.paypal.com"; }
        }

        public static string BusinessEmail
        {
            get { return ConfigurationManager.AppSettings["Paypal:BusinessEmail"]; }
        }

        public static string AuthToken
        {
            get { return ConfigurationManager.AppSettings["Paypal:AuthToken"]; }
        }

    }
}