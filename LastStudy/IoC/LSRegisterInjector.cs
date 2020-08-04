using LastStudy.Core.Interfaces.DependencyInjector;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using SimpleInjector.Integration.WebApi;

namespace LastStudy.IoC
{
    public class LSRegisterInjector
    {
        public static Container CreateContainer()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<IServiceLocator, ServiceLocator>(Lifestyle.Scoped);

            RegisterNamespaces(container, null,
               "DataAccessLayer.UnitOfWork",
               "DataAccessLayer.Repositories",
               "BusinessLogicLayer.Objects");

            // need to register further classes
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();
            return container;
        }

        private static void RegisterNamespaces(Container container, Lifestyle lifestyle, params string[] s_namespaces)
        {
            HashSet<string> namespaces = new HashSet<string>(s_namespaces);
            HashSet<string> assemblyNames = new HashSet<string>(s_namespaces);

            foreach (string namespace_inner in namespaces)
            {
                string assemblyName = namespace_inner.Split('.')[0]; // gets the DataAccessLayer of DataAccessLayer.UnitOfWork
                if (!assemblyNames.Contains(assemblyName))
                {
                    assemblyNames.Add(assemblyName);
                }
            }

            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => assemblyNames.Contains(x.GetName().Name)); // Debug this to get the idea!

            Type iDisposable = typeof(IDisposable);
            Lifestyle currentLifestyle = lifestyle; // might use this later 
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetExportedTypes().Where(x => namespaces.Contains(x.Namespace) && !x.IsInterface && !x.IsAbstract).ToArray();
                // from viewbox ;)
                foreach (Type type in types)
                {
                    Type _interface = type.GetInterface("I" + type.Name);
                    ConstructorInfo[] constructors = type.GetConstructors(); //Reflection - Pro C#5 pg 560
                    if (_interface != null && constructors.Length > 0 && constructors.All(x => x.IsPublic))
                    {
                        currentLifestyle = lifestyle; // not used in exams project yet!

                        if (currentLifestyle == null)
                        {
                            if (iDisposable.IsAssignableFrom(type)) // https://simpleinjector.readthedocs.io/en/latest/lifetimes.html#
                            {
                                currentLifestyle = Lifestyle.Scoped;
                            }
                            else
                            {
                                currentLifestyle = Lifestyle.Transient;
                            }
                        }

                        if (_interface.IsGenericType && !_interface.IsGenericTypeDefinition)
                            _interface = _interface.GetGenericTypeDefinition();
                        container.Register(_interface, type, currentLifestyle);
                    }


                }
            }

        }
    }
}