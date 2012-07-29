using StructureMap;
using Raven.Client;
using SupportSasha.Donations.Controllers;
namespace SupportSasha.Donations {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });

                            x.For<IDocumentSession>().HttpContextScoped().Use(() => BaseController.DocumentStore.OpenSession());
                       });
            return ObjectFactory.Container;
        }
    }
}