
namespace Examples.Ci.WebApi.Infrastructures
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using System.Diagnostics.CodeAnalysis;


    [ExcludeFromCodeCoverage]
    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // TODO: injection object in this
        }
    }
}