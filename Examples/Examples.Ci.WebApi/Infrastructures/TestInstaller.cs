
namespace Examples.Ci.WebApi.Infrastructures
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Controllers;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class TestInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            #region code examples
            //container.Register(Component.For<ITest>().ImplementedBy<Test>().DependsOn(Dependency.OnValue("message", "hello, world!")).LifestylePerWebRequest());

            //container.Register(Component.For<ISettings>().ImplementedBy<Settings>().LifestyleSingleton());

            //var settings = container.Resolve<ISettings>();

            //Debug.WriteLine(settings);

            //container.Register(Component.For<ITest>().ImplementedBy<Test>().DependsOn(Dependency.OnComponent(typeof(ISettings), typeof(Settings).FullName)).DependsOn(Dependency.OnValue("message", "123")).OnCreate(a =>
            //{
            //    var settings = container.Resolve<ISettings>();
            //    Debug.WriteLine(settings);
            //}).LifestylePerWebRequest());
            #endregion

            #region enable priority from ioc
            //var settings = container.Resolve<Utils.ISettings>();
            //System.Diagnostics.Debug.WriteLine(settings);
            //container.Register(Component.For<ITest>().ImplementedBy<Test>().DependsOn(Dependency.OnValue("message", settings.DefaultConnectionString)).LifestylePerWebRequest());
            #endregion

            container.Register(Component.For<ITest>().ImplementedBy<Test>().LifestylePerWebRequest());
        }
    }
}