using StructureMap;
using Griffin.MvcContrib.Providers.Membership.PasswordStrategies;
using Griffin.MvcContrib.RavenDb.Providers;
using Griffin.MvcContrib.Providers.Membership;
using Griffin.MvcContrib.Providers.Roles;
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
                            x.For<IPasswordStrategy>().Use<HashPasswordStrategy>();
                            x.For<IAccountRepository>().Use<RavenDbAccountRepository>();
                            x.For<IRoleRepository>().Use<RavenDbRoleRepository>();
                            x.For < IPasswordPolicy>().Singleton().Use(new PasswordPolicy
                            {
                                IsPasswordQuestionRequired = false,
                                IsPasswordResetEnabled = true,
                                IsPasswordRetrievalEnabled = false,
                                MaxInvalidPasswordAttempts = 5,
                                MinRequiredNonAlphanumericCharacters = 0,
                                PasswordAttemptWindow = 10,
                                PasswordMinimumLength = 6,
                                PasswordStrengthRegularExpression = null
                            });
            
                        });
            return ObjectFactory.Container;
        }
    }
}