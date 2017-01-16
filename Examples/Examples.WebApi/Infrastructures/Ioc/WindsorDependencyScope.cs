
namespace Examples.WebApi.Infrastructures
{
    using Castle.MicroKernel;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Http.Dependencies;

    [ExcludeFromCodeCoverage]
    public class WindsorDependencyScop : IDependencyScope
    {
        #region IDependencyScope Implements
        public void Dispose()
        {
            if (container.Parent != null)
            {
                container.RemoveChildKernel(container);
            }

            container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return container.HasComponent(serviceType) ? container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType).Cast<object>();
        }
        #endregion

        protected IKernel container;

        public WindsorDependencyScop(IKernel container)
        {
            this.container = container;
        }
    }
}