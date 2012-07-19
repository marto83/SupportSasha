using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SupportSasha.Donations.Models;
using SupportSasha.Donations.Code;

namespace SupportSasha.Donations.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ISignInService _service;
        public AccountController(ISignInService service)
        {
            _service = service;            
        }


        [ActionName("sign-in")]
        public ActionResult SignIn(string returnUrl)
        {
            return View(new SignInModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ActionName("sign-in")]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                User user;
                if (SignInUser(model, out user))
                {
                    _service.SignIn(user, model.RememberMe);
                    return Redirect(GetRedirectUrl(model.ReturnUrl));
                }
                else
                    ModelState.AddModelError("password", "Wrong username or password!");
            }
            return View(model);
        }

        private bool SignInUser(SignInModel model, out User user)
        {
            user = Session.Query<User>().FirstOrDefault(x => x.Name == model.Username);
            return user != null && user.Password == model.Password;
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return returnUrl;

            return "/";
        }

        [ActionName("sign-out")]
        public ActionResult SignOut()
        {
            _service.SignOut();
            return Redirect("/");
        }
    }
}
