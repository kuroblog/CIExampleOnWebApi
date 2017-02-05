
namespace Examples.Ci.WebApi.Infrastructures.Ioc.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using System.Web.Http;

    public class ApiControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleTransient());
        }
    }
}