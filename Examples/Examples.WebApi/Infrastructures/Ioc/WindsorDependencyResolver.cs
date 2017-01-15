
namespace Examples.WebApi.Infrastructures
{
    using Castle.MicroKernel;
    using Ms = System.Web.Http.Dependencies;

    public class WindsorDependencyResolver : WindsorDependencyScop, Ms.IDependencyResolver
    {
        public WindsorDependencyResolver(IKernel container) : base(container) { }

        public Ms.IDependencyScope BeginScope()
        {
            container.AddChildKernel(new DefaultKernel());

            return new WindsorDependencyScop(container);
        }
    }
}