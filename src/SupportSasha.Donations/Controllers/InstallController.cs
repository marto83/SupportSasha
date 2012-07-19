﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Griffin.MvcContrib.Providers.Membership;
using Griffin.MvcContrib.Providers.Roles;
using Griffin.MvcContrib.RavenDb.Providers;
using SupportSasha.Donations.Models;

namespace SupportSasha.Donations.Controllers
{
    public class InstallController : BaseController
    {
        private readonly IAccountRepository _Accounts;
        private readonly IRoleRepository _Roles;
        /// <summary>
        /// Initializes a new instance of the InstallController class.
        /// </summary>
        public InstallController(IAccountRepository accounts, IRoleRepository roles)
        {
            _Roles = roles;
            _Accounts = accounts;            
        }

        public ActionResult Index()
        {
               //create admin role
                //_Roles.CreateRole(MvcApplication.ApplicationName, "Admin");
            var user =Session.Query<User>().FirstOrDefault(x => x.Name == "admin");
            if (user == null)
            {
                User admin = new User { Name = "admin", Email = "marto83@gmail.com", Password = "password" };
                Session.Store(admin);
                TempData["message"] = "Admin user setup";
            }
                
            return Redirect("/");
        }

        public string Test ()
        {
            return "testing";
        }

    }
}
