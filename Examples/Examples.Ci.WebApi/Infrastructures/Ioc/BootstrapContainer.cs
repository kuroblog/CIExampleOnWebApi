
namespace Examples.Ci.WebApi.Infrastructures.Ioc
{
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class BootstrapContainer
    {
        public static IWindsorContainer Container { get; set; }

        static BootstrapContainer()
        {
            #region code examples
            // default install
            Container = new WindsorContainer().Install(FromAssembly.This());
            #endregion

            #region enable priority from ioc
            // using install by priority
            //Container = new WindsorContainer().Install(FromAssembly.This(new WindsorInstallerBootstrap()));
            #endregion
        }
    }
}