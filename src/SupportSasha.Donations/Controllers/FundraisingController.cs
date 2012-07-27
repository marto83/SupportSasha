using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportSasha.Donations.Models;

namespace SupportSasha.Donations.Controllers
{
    public class FundraisingController : BaseController
    {
        public ActionResult Swim()
        {
            var donations = RavenSession.Query<Donation>().Where(x => x.CampaignName == "Swim").ToList();
            var model = new SwimProgressModel();
            model.Donations = donations;
            model.PurchasedSwimLanes = (int)Math.Round(donations.Sum(x => x.Amount), 0);
            return View(model);
        }

    }

    
}
