using System;
using System.Linq;
using SupportSasha.Donations.Models;

namespace SupportSasha.Donations.Code
{
    public interface ISignInService
    {
        void SignIn(User user, bool rememberMe);
        void SignOut();
    }
}
