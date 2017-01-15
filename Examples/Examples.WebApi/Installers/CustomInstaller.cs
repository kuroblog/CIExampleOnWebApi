﻿
namespace Examples.WebApi.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Controllers;
    using Models;
    using Repositories;
    using System.Data.Entity;

    public class CustomInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMessages>().ImplementedBy<Messages>().LifestyleSingleton());

            container.Register(Component.For<DbContext>().ImplementedBy<ExampleDbContext>().LifestylePerWebRequest());
            //container.Register(Component.For<DbContext>().ImplementedBy<ExampleDbContext>().LifestyleTransient());

            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleTransient());
        }
    }
}