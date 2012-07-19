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
            var donations = Session.Query<Donation>().ToList();
            return View(donations);
        }

        public ActionResult Create()
        {
            return View();    
        }

        [HttpPost]
        public ActionResult Create(DonationInput model)
        {
            if(ModelState.IsValid)
            {
                var donation = new Donation() { Name = model.Name, Amount = model.Amount, Message = model.Message, Email = model.Email };
                Session.Store(donation);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
