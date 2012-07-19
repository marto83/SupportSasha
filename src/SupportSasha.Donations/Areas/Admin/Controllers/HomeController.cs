using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportSasha.Donations.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : AdminController
    {
        //
        // GET: /Admin/Admin/

        public ActionResult Index()
        {
            return View();
        }

    }
}
