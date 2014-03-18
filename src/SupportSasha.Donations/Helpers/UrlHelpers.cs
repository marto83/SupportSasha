using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace SupportSasha.Donations.Helpers
{
    public static class UrlHelpers
    {
        public static dynamic Javascript(this UrlHelper helper, string path)
        {
            return helper.Content("~/Content/Scipts/" + path);
        }

        public static dynamic Css(this UrlHelper helper, string path)
        {
            return helper.Content("~/Content/Styles/" + path);
        }

        public static dynamic Image(this UrlHelper helper, string path)
        {
            return helper.Content("~/Content/Images/" + path);
        }

        public static string SiteRoot(this UrlHelper helper, bool includeAppPath = true, string scheme = "")
        {
            var context = HttpContext.Current;
            string Port;

            if (scheme.IsEmpty())
            {
                Port = context.Request.ServerVariables["SERVER_PORT"];
                if (Port == null || Port == "80" || Port == "443")
                    Port = "";
                else
                    Port = ":" + Port;
            }
            else
            {
                Port = ":" + (scheme == "http" ? "80" : "443");
            }

            string Protocol;
            if (scheme.IsEmpty())
            {
                Protocol = context.Request.ServerVariables["SERVER_PORT_SECURE"];
                if (Protocol == null || Protocol == "0")
                    Protocol = "http://";
                else
                    Protocol = "https://";
            }
            else
            {
                if (scheme == "http")
                    Protocol = "http://";
                else
                    Protocol = "https://";
            }

            var appPath = "";
            if (includeAppPath)
            {
                appPath = context.Request.ApplicationPath;
                if (appPath == "/")
                    appPath = "";
            }
            var sOut = Protocol + GetHost(context) + Port + appPath;
            return sOut;
        }

        private static string GetHost(HttpContext context)
        {
            try
            {
                return context.Request.ServerVariables["SERVER_NAME"];
            }
            catch (Exception)
            {
                return "localhost";
            }
        }
    }
}
