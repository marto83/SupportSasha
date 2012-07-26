using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace SupportSasha.Donations
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly string ApplicationName = "SupportSasha";
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "THank you", // Route name
                "Donations/thankyou", // URL with parameters
                new { controller = "Donations", action = "ThankYou" },
                null,
                new string[] { "SupportSasha.Donations.Controllers" }
            );

            routes.MapRoute(
                "Donation targets", // Route name
                "Donations/Targets", // URL with parameters
                new { controller = "Donations", action = "Targets" },
                null,
                new string[] { "SupportSasha.Donations.Controllers" }
            );

            routes.MapRoute(
                "Donations", // Route name
                "Donations/{campaign}", // URL with parameters
                new { controller = "Donations", action = "Index", campaign = UrlParameter.Optional },
                null,
                new string[] { "SupportSasha.Donations.Controllers" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                null, 
                new string[] {"SupportSasha.Donations.Controllers"}
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            
            ControllerBuilder.Current.SetControllerFactory(typeof(StructureMapControllerFactory));
        }
    }
}