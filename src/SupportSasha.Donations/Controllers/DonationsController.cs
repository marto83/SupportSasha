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
            return View();
        }

        [HttpPost]
        public ActionResult Index(DonationInput attempt)
        {
            if (ModelState.IsValid)
            {
                Donation donation = Donation.CreatFromInput(attempt);
                RavenSession.Store(donation);
                RavenSession.SaveChanges();
                Session[DonationIDKey] = donation.Id;

                string paypalUrl = GetPaypalUrl(attempt);
                return Redirect(paypalUrl);
            }

            return View(attempt);
        }

        private string GetPaypalUrl(DonationInput attempt)
        {
            UriBuilder builder = new UriBuilder(String.Format("{0}/cgi-bin/webscr", PaypalSettings.PaypalUrl));
            NameValueCollection query = new NameValueCollection();
            query["cmd"] = "_donations";
            query["business"] = PaypalSettings.BusinessEmail;
            query["lc"] = "GB";
            query["item_name"] = "Support Sasha";
            query["item_number"] = attempt.Campaign;
            query["currency_code"] = "GBP";
            query["bn"] = "PP-DonationsBF:btn_donateCC_LG.gif:NonHosted";
            query["amount"] = attempt.Amount.ToString();
            //query["return"] = WebHelpers.ResolveServerUrl("/donations/thankyou");
            builder.Query = ToQueryString(query);
            return builder.Uri.ToString();
        }

        public PaypalDataResult ValidateDataTransfer()
        {
            // CUSTOMIZE THIS: This is the seller's Payment Data Transfer authorization token.
            // Replace this with the PDT token in "Website Payment Preferences" under your account.
            string txToken = Request.QueryString["tx"];
            string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + PaypalSettings.AuthToken;

            //Post back to either sandbox or live
            string url = String.Format("{0}/cgi-bin/webscr", PaypalSettings.PaypalUrl);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            //Send the request to PayPal and get the response
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {
                streamOut.Write(query);
                streamOut.Close();
            }
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            Dictionary<string, string> results = new Dictionary<string, string>();
            if (strResponse != "")
            {
                StringReader reader = new StringReader(strResponse);
                string line = reader.ReadLine();

                if (line == "SUCCESS")
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        results.Add(line.Split('=')[0], line.Split('=')[1]);
                    }

                    return new PaypalDataResult { Success = true, Name = results["first_name"] + " " + results["last_name"], Amount = results["payment_gross"] };
                }
                else if (line == "FAIL")
                {
                    return new PaypalDataResult { Success = false };
                }
            }


            return new PaypalDataResult { Success = false };
        }

        public ActionResult ThankYou(decimal? amt, bool test = false)
        {
            dynamic model = new ExpandoObject();
            if (test)
            {
                model.Name = "Testing";
                return View(model);
            }

            string donationId = DonationId;
            if (string.IsNullOrEmpty(donationId))
            {
                return Redirect("/");
            }

            var result = ValidateDataTransfer();
            var donation = RavenSession.Load<Donation>(donationId);

            if (result.Success)
            {
                if (amt.HasValue)
                {
                    if (amt == donation.Amount)
                        donation.Confirmed = true;
                    else
                        donation.Amount = amt.Value;

                    donation.Confirmed = true;
                }
            }

            //Start in a new thread so it doesn't slow down the thank you message
            Task.Factory.StartNew(() =>
            {
                var mailer = new Mailer();
                mailer.SendThankyouEmail(donation.Email);
                mailer.SendNotificationEmail(donation.Name);
            });

            DonationId = null;


            model.Name = donation.Name;
            return View(model);
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
