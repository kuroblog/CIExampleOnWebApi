
namespace Examples.Ci.WebApi.Infrastructures.Ioc
{
    using Castle.MicroKernel;
    using Castle.MicroKernel.Lifestyle;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Http.Dependencies;

    [ExcludeFromCodeCoverage]
    public class DependencyScop : IDependencyScope
    {
        #region IDependencyScope
        public object GetService(Type type)
        {
            try
            {
                return kernel.HasComponent(type) ? kernel.Resolve(type) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<object> GetServices(Type type)
        {
            try
            {
                return kernel.ResolveAll(type).Cast<object>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region IDisposable
        public virtual void Dispose()
        {
            disposable.Dispose();
        }
        #endregion

        protected readonly IKernel kernel;
        private readonly IDisposable disposable;

        public DependencyScop(IKernel kernel)
        {
            this.kernel = kernel;
            disposable = kernel.BeginScope();
        }
    }
}