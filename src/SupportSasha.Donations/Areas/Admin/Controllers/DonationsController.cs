using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SupportSasha.Donations.Models;

namespace SupportSasha.Donations.Areas.Admin.Controllers
{
    public class DonationsController : AdminController
    {
        public ActionResult Index()
        {
            var donations = Session.Query<DonationAttempt>().ToList();
            return View(donations);
        }
    }
}
