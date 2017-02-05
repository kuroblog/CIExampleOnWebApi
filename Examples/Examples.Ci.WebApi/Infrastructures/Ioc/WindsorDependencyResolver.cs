
namespace Examples.Ci.WebApi.Infrastructures.Ioc
{
    using Castle.MicroKernel;
    using System.Diagnostics.CodeAnalysis;
    using Ms = System.Web.Http.Dependencies;

    [ExcludeFromCodeCoverage]
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