using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
    }
}
