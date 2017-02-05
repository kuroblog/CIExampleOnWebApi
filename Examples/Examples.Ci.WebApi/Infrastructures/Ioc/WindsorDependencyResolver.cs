
namespace Examples.Ci.WebApi.Infrastructures.Ioc
{
    using Castle.MicroKernel;
    using Ms = System.Web.Http.Dependencies;

    public class WindsorDependencyResolver : WindsorDependencyScop, Ms.IDependencyResolver
    {
        #region WindsorDependencyScop Implements
        public WindsorDependencyResolver(IKernel container) : base(container) { }
        #endregion

        #region Ms.IDependencyResolver Implements
        public Ms.IDependencyScope BeginScope()
        {
            container.AddChildKernel(new DefaultKernel());

            return new WindsorDependencyScop(container);
        }
        #endregion
    }
}