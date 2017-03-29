
namespace Examples.Ci.WebApi.Infrastructures
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Ef;
    using Ef.Repositories;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class UserInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            #region code examples
            //container.Register(Component.For<IMessages>().ImplementedBy<Messages>().LifestylePerWebRequest());
            //container.Register(Component.For<IMessages>().ImplementedBy<Messages>().LifestyleSingleton());

            //container.Register(Component.For<DbContext>().ImplementedBy<ExampleDbContext>().LifestylePerWebRequest());
            //container.Register(Component.For<DbContext>().ImplementedBy<ExampleDbContext>().LifestyleTransient());
            #endregion

            container.Register(Component.For<DbContext>().ImplementedBy<CiContext>().LifestylePerWebRequest());
            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestylePerWebRequest());
        }
    }
}