using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportSasha.Donations.Models;
using System.Collections.Specialized;
using System.Collections;
using SupportSasha.Donations.Helpers;
using System.Net;
using System.IO;
using SupportSasha.Donations.Code;
using System.Dynamic;
using System.Threading.Tasks;

namespace SupportSasha.Donations.Controllers
{
    public class PaypalDataResult
    {
        public bool Success { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
    }

    public class DonationsController : BaseController
    {
        private const string DonationIDKey = "DonationId";

        public string DonationId
        {
            get { return (string)Session[DonationIDKey]; }
            set { Session[DonationIDKey] = value; }
        }

        public ActionResult Index(string campaign = "")
        {
            return Redirect("http://www.justgiving.com/supportsasha/donate?amount=25&exitUrl=" + Url.Encode(Url.SiteRoot()));
        }

        

        public ActionResult ThankYou(decimal? amt, bool test = false)
        {
            return View();
        }


        public ActionResult Targets()
        {
            var targets = RavenSession.Query<Target>().ToList();
            return View(targets);
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            return string.Join("&", Array.ConvertAll(nvc.AllKeys, key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(nvc[key]))));
        }
    }
}
