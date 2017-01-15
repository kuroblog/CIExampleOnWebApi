
namespace Examples.WebApi.Infrastructures
{
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class BootstrapContainer
    {
        public static IWindsorContainer Container { get; set; }

        static BootstrapContainer()
        {
            Container = new WindsorContainer().Install(FromAssembly.This());
        }
    }
}