
namespace Examples.Ci.WebApi
{
    using Ef;
    using Infrastructures.Ioc;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Http;

    [ExcludeFromCodeCoverage]
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CiContext, Ef.Migrations.Configuration>());

            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.DependencyResolver = new DependencyResolver(BootstrapContainer.Container.Kernel);
        }
    }
}
