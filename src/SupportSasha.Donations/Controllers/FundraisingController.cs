using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SupportSasha.Donations.Models;

namespace SupportSasha.Donations.Controllers
{
    public class FundraisingController : BaseController
    {
        public ActionResult Swim()
        {
            var donations = RavenSession.Query<Donation>().Where(x => x.CampaignName == "Swim" && x.Confirmed).OrderByDescending(x => x.Date).ToList();
            var model = new EventProgressModel();
            model.Donations = donations.OrderByDescending(x => x.Date);
            model.Target = 1000;
            model.CurrentAmount = (int)Math.Round(donations.Sum(x => x.Amount), 0);
            return View(model);
        }

        public ActionResult Jump()
        {
            return View();
        }
    }
}
