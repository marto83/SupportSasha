using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportSasha.Donations.Models;
using SupportSasha.Donations.Controllers;

namespace SupportSasha.Donations.Areas.Admin.Controllers
{
    public class TargetsController : AdminController
    {
        //
        // GET: /Admin/Targets/

        public ActionResult Index()
        {
            var targets = RavenSession.Query<Target>().ToList();
            return View(targets);
        }

        //
        // GET: /Admin/Targets/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Targets/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Targets/Create

        [HttpPost]
        public ActionResult Create(Target target)
        {
            try
            {
               RavenSession.Store(target);
               RavenSession.SaveChanges();
               SetMessage("Target saved");

               return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Targets/Edit/5
 
        public ActionResult Edit(string id)
        {
            var fullId = "targets/" + id;
            var target = RavenSession.Load<Target>(fullId);
            return View(target);
        }

        //
        // POST: /Admin/Targets/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, Target target)
        {
            try
            {
                string fullId = "targets/" + id;
                var dbTarget = RavenSession.Load<Target>(fullId);
                dbTarget.Name = target.Name;
                dbTarget.Amount = target.Amount;
                dbTarget.MoreInfoLink = target.MoreInfoLink;
                dbTarget.Description = target.Description;
                RavenSession.SaveChanges();

                SetMessage("Target updated successfully");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var target = RavenSession.Load<Target>(id);
                RavenSession.Delete(target);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
