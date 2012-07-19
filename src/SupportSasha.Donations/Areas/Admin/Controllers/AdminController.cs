using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportSasha.Donations.Controllers;

namespace SupportSasha.Donations.Areas.Admin.Controllers
{
    [Authorize]
    public abstract class AdminController : BaseController
    {
        

    }
}
