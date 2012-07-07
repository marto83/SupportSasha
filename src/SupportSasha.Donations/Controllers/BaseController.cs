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
        public new IDocumentSession Session { get; set; }

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
                        ConnectionStringName = "RavenDB"
                    }.Initialize();

                    return _documentStore;
                }
            }
        }

        protected void SetMessage(string message)
        {
            ViewBag.Message = message;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Session = DocumentStore.OpenSession();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (Session)
            {
                if (Session != null && filterContext.Exception == null)
                    Session.SaveChanges();
            }
        }
    }
}