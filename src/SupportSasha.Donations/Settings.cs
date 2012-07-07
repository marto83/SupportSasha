using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace SupportSasha.Donations
{
    public static class Settings
    {
        public static bool InDevelopment
        {
            get { return ConfigurationManager.AppSettings["InDevelopment"] == "true"; }
        }
    }
}