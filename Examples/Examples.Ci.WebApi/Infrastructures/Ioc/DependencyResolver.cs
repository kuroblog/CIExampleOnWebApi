
namespace Examples.Ci.WebApi.Infrastructures.Ioc
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Http.Dependencies;
    using Ioc = Castle.MicroKernel;

    [ExcludeFromCodeCoverage]
    public class DependencyResolver : DependencyScop, IDependencyResolver
    {
        #region IDependencyResolver
        public IDependencyScope BeginScope()
        {
            return new DependencyScop(kernel);
        }
        #endregion

        public DependencyResolver(Ioc.IKernel kernel) : base(kernel) { }
    }
}