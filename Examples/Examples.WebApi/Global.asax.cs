
namespace Examples.WebApi
{
    using Infrastructures;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(BootstrapContainer.Container.Kernel);
        }
    }
}
