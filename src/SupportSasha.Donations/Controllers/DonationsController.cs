using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportSasha.Donations.Models;
using System.Collections.Specialized;

namespace SupportSasha.Donations.Controllers
{
    public class DonationsController : BaseController
    {
        public ActionResult Index(string campaign = "")
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DonationAttempt attempt)
        {
            attempt.Date = DateTimeOffset.Now.DateTime;
            Session.Store(attempt);
            var campaign = attempt.CampaignName ?? "Donation";

            UriBuilder builder = new UriBuilder("https://www.paypal.com/cgi-bin/webscr");
            NameValueCollection query = new NameValueCollection();
            query["cmd"] = "_donations";
            query["business"] = "kategeorgiev@yahoo.co.uk";
            query["lc"] = "GB";
            query["item_name"] = "Support Sasha";
            query["item_number"] = campaign;
            query["currency_code"] = "GBP";
            query["bn"] = "PP-DonationsBF:btn_donateCC_LG.gif:NonHosted";
            query["amount"] = attempt.Amount.ToString();
            query["return"] = "http://supportsasha.apphb.com/donations/thankyou";
            builder.Query = ToQueryString(query);
            return Redirect(builder.Uri.ToString());
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            return string.Join("&", Array.ConvertAll(nvc.AllKeys, key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(nvc[key]))));
        }
    }
}
