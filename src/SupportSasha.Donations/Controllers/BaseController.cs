using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using System.Web.Mvc;
using Raven.Client.Embedded;

namespace SupportSasha.Donations.Controllers
{
    public class BaseController : Controller
    {
        public new IDocumentSession RavenSession { get; set; }

        private static IDocumentStore _documentStore;
        public static IDocumentStore DocumentStore
        {
            get
            {
                if (_documentStore != null)
                    return _documentStore;

                lock (typeof(BaseController))
                {
                    if (_documentStore != null)
                        return _documentStore;

                    _documentStore = new EmbeddableDocumentStore
                    {
                        ConnectionStringName = "RavenDB"//,
                       // UseEmbeddedHttpServer = true
                    }.Initialize();

                    return _documentStore;
                }
            }
        }

        protected void SetMessage(string message, params object[] args)
        {
            TempData["message"] = String.Format(message, args);
        }

        protected void SetError(string error, params object[] args)
        {
            TempData["error"] = String.Format(error, args);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = DocumentStore.OpenSession();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (RavenSession)
            {
                if (RavenSession != null && filterContext.Exception == null)
                    RavenSession.SaveChanges();
            }
        }
    }
}