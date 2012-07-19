using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportSasha.Donations.Models;

namespace SupportSasha.Donations.Controllers
{


    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            IEnumerable<Target> targets = Session.Query<Target>().ToList();
            IEnumerable<Donation> donations = Session.Query<Donation>().Where(x => x.Confirmed);
            HomeViewModel model = new HomeViewModel();
            decimal targetsSum = targets.Sum(x => x.Amount);
            model.TargetsTotal = targetsSum;
            model.Targets = targets;
            model.Donations = donations;
            decimal donationsSum = donations.Sum(x => x.Amount);
            model.ProgressPercentage = (double)Math.Round(donationsSum / targetsSum * 100, 2);
            return View(model);
        }
    }
}
