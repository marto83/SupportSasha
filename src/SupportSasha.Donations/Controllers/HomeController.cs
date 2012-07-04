using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportSasha.Donations.Models;

namespace SupportSasha.Donations.Controllers
{
    

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Target> targets = GetTargets();
            IEnumerable<Donation> donations = GetDonations();
            HomeViewModel model = new HomeViewModel();
            decimal targetsSum = targets.Sum(x => x.Amount);
            model.TargetsTotal = targetsSum;
            model.Targets = targets;
            model.Donations = donations;
            decimal donationsSum = donations.Sum(x => x.Amount);
            model.ProgressPercentage = (double)Math.Round(donationsSum / targetsSum * 100, 2);
            return View(model);
        }

        private IEnumerable<Target> GetTargets()
        {
            return new List<Target> { 
                new Target { Amount = 5000, Description = "Dolphin therapy"},
                new Target { Amount = 11000, Description = "Stem cell therapy" }
            };
        }

        private IEnumerable<Donation> GetDonations()
        {
            return new List<Donation>
            {
                new Donation { Name = "Martin", Message = "Good luck Sasha", Amount = 10 },
                new Donation { Name = "George", Message = "Enjoy swimming with the dolphins", Amount = 40 },
                new Donation { Name = "Lindsey", Message = "", Amount = 100 },
                new Donation { Name = "Anonymous", Message = "", Amount = 200 }
            };
        }
    }
}
