using LastStudy.Core.Interfaces.DependencyInjector;

using SimpleInjector;
//using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastStudy.IoC
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Container _container;
        private Scope _scope;

        public ServiceLocator(Container container)
        {
            this._container = container;
        }
        public void BeginScope()
        {
            _scope = AsyncScopedLifestyle.BeginScope(_container);
        }

        public void EndScope()
        {
            _scope.Dispose();
        }

        public T Resolve<T>() where T : class
        {
            return this._container.GetInstance<T>();
        }
    }


}