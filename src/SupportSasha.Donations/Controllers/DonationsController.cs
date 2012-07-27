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

namespace SupportSasha.Donations.Controllers
{
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
            UriBuilder builder = new UriBuilder("https://www.paypal.com/cgi-bin/webscr");
            NameValueCollection query = new NameValueCollection();
            query["cmd"] = "_donations";
            query["business"] = "kategeorgiev@yahoo.co.uk";
            query["lc"] = "GB";
            query["item_name"] = "Support Sasha";
            query["item_number"] = attempt.Campaign;
            query["currency_code"] = "GBP";
            query["bn"] = "PP-DonationsBF:btn_donateCC_LG.gif:NonHosted";
            query["amount"] = attempt.Amount.ToString();
            query["return"] = WebHelpers.ResolveServerUrl("/donations/thankyou");
            builder.Query = ToQueryString(query);
            return builder.Uri.ToString();
        }

        public void ValidateDataTransfer()
        {
            // CUSTOMIZE THIS: This is the seller's Payment Data Transfer authorization token.
            // Replace this with the PDT token in "Website Payment Preferences" under your account.
            string authToken = "VLffXfMn3q62BhuZuKob-f87J1-FQBUtR97kuj9kMSwfI_zR4v4lRYFciDq";
            string txToken = Request.QueryString["tx"];
            string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + authToken;

            //Post back to either sandbox or live
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://www.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strLive);

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
                    Response.Write("<p><h3>Your order has been received.</h3></p>");
                    Response.Write("<b>Details</b><br>");
                    Response.Write("<li>Name: " + results["first_name"] + " " + results["last_name"] + "</li>");
                    Response.Write("<li>Item: " + results["item_name"] + "</li>");
                    Response.Write("<li>Amount: " + results["payment_gross"] + "</li>");
                    Response.Write("<hr>");
                }
                else if (line == "FAIL")
                {
                    // Log for manual investigation
                    Response.Write("Unable to retrive transaction detail");
                }
            }
            else
            {
                //unknown error
                Response.Write("ERROR");
            }            
        }

        public ActionResult ThankYou()
        {
            string donationId = DonationId;
            if (string.IsNullOrEmpty(donationId))
            {
               return Redirect("/");
            }

            var donation = RavenSession.Load<Donation>(donationId);

            donation.Confirmed = true;
            //send thank you email
            //clear donation Id
            DonationId = null;

           return View(new { Name = donation.Name });
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
