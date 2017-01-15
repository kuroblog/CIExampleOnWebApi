
namespace Examples.WebApi
{
    using Infrastructures;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Http;

    [ExcludeFromCodeCoverage]
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(BootstrapContainer.Container.Kernel);
        }
    }
}
