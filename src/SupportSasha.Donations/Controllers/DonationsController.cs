using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportSasha.Donations.Models;
using System.Collections.Specialized;

namespace SupportSasha.Donations.Controllers
{
    public class DonationsController : Controller
    {
        //
        // GET: /Donate/

        public ActionResult Index(string campaign = "")
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(DonationAttempt attempt)
        {
            UriBuilder builder = new UriBuilder("https://www.sandbox.paypal.com/cgi-bin/webscr");
            NameValueCollection query = new NameValueCollection();
            query["cmd"] = "_donations";
            query["business"] = "user_1341349220_per@gmail.com";
            query["lc"] = "GB";
            query["item_name"] = "Support Sasha";
            query["item_number"] = "test";
            query["currency_code"] = "GBP";
            query["bn"] = "PP-DonationsBF:btn_donateCC_LG.gif:NonHosted";
            query["amount"] = "10";
            query["return"] = "http://supportsasha.apphb.com/donations/thankyou";
            builder.Query = ToQueryString(query);
            return Redirect(builder.Uri.ToString());
        }

        public ActionResult ThankYou()
        {
                var id = this.Request.QueryString["tx"];
				var st = this.Request.QueryString["st"];
			    var amount = this.Request.QueryString["amt"];
		        var cc = this.Request.QueryString["cc"];

                return Content(String.Format("id: {0}, status: {1}, amount: {2}, cc: {3}", id, st, amount, cc));
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            return string.Join("&", Array.ConvertAll(nvc.AllKeys, key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(nvc[key]))));
        }
    }
}
