
namespace Examples.Ci.WebApi.Infrastructures
{
    using Attributes;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Http;
    using Utils;

    [InstallerPriority(0), ExcludeFromCodeCoverage]
    public class BootstrapInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // api controller
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());

            // settings
            container.Register(Component.For<ISettings>().ImplementedBy<Settings>().LifestyleSingleton());
        }
    }
}