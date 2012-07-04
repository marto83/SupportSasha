using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}
