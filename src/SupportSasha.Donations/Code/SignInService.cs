using System;
using System.Linq;
using System.Web.Security;
using System.Web;
using Raven.Imports.Newtonsoft.Json;
using SupportSasha.Donations.Models;

namespace SupportSasha.Donations.Code
{
    public class SignInService : ISignInService
    {
        public void SignIn(User user, bool rememberMe)
        {
            SetAuthCookie<object>(user.Name, 
                rememberMe, null);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public void SetAuthCookie<T>(string name, bool rememberMe, T userData) where T : class
        {
            /// In order to pickup the settings from config, we create a default cookie and use its values to create a
            /// new one.
            var cookie = FormsAuthentication.GetAuthCookie(name, rememberMe);
            if(userData != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);


                var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration,
                    ticket.IsPersistent, JsonConvert.SerializeObject(userData), ticket.CookiePath);
                var encTicket = FormsAuthentication.Encrypt(newTicket);

                cookie.Value = encTicket;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}

