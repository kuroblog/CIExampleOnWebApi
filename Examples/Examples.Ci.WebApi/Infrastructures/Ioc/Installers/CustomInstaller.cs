
namespace Examples.Ci.WebApi.Infrastructures.Ioc.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Ef;
    using Ef.Repositories;
    using System.Data.Entity;

    public class CustomInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component.For<IMessages>().ImplementedBy<Messages>().LifestylePerWebRequest());
            //container.Register(Component.For<IMessages>().ImplementedBy<Messages>().LifestyleSingleton());

            //container.Register(Component.For<DbContext>().ImplementedBy<ExampleDbContext>().LifestylePerWebRequest());
            //container.Register(Component.For<DbContext>().ImplementedBy<ExampleDbContext>().LifestyleTransient());
            container.Register(Component.For<DbContext>().ImplementedBy<CiContext>().LifestyleSingleton());

            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleTransient());
        }
    }
}