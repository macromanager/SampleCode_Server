using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using Infrastructure.IocInstallers;
using SimpleInjector.Lifestyles;

namespace MacroContext.Infrastructure
{
    public static class Bootstrapper
    {
        public static readonly Container Container;


        static Bootstrapper()
        {
            Container = new Container();
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
        }

        public static void RegisterServices()
        {
            InfrastructureInstaller.RegisterServices(Container);
            ApplicationServicesInstaller.RegisterServices(Container);
            PersistanceInstaller.RegisterServices(Container);
        }

        public static void VerifyContainer()
        {
            Container.Verify();
        }

        public static T GetInstance<T>() where T : class
        {
            var instance = Bootstrapper.Container.GetInstance<T>();
            return instance;
        }

        public static void DisposeResources()
        {
            if (Container != null)
            {
                Container.Dispose();
            }
        }

    }
}
