
namespace Examples.Ci.WebApi
{
    using Infrastructures.Ioc;
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
