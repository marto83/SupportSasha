using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportSasha.Donations.Models;
using SupportSasha.Donations.Controllers;

namespace SupportSasha.Donations.Areas.Admin.Controllers
{
    public class TargetsController : BaseController
    {
        //
        // GET: /Admin/Targets/

        public ActionResult Index()
        {
            var targets = Session.Query<Target>().ToList();
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
               Session.Store(target);
               Session.SaveChanges();
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
            return View();
        }

        //
        // POST: /Admin/Targets/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
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
                var target = Session.Load<Target>(id);
                Session.Delete(target);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
