using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Persistance;
using SimpleInjector;
using System.Data.Entity;
using SimpleInjector.Diagnostics;
using SimpleInjector.Lifestyles;
using MacroContext.Infrastructure.Abstractions.Orm;
using System.Configuration;

namespace Infrastructure.IocInstallers
{
    static class PersistanceInstaller
    {
        private static Container _container;

        public static void RegisterServices(Container _simpleContainer)
        {
            _container = _simpleContainer;
            _simpleContainer.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            var connectionString = ConfigurationManager.ConnectionStrings["MacroContextDb"].ConnectionString;
            _simpleContainer.Register<MacroContextDb>(()=> new MacroContextDb(connectionString),Lifestyle.Scoped);
            _simpleContainer.Register<IMacroContextDb>(()=>_simpleContainer.GetInstance<MacroContextDb>(), Lifestyle.Scoped);
            RegisterRepositories();
        }


        private static void RegisterRepositories()
        {
            var repoAssembly = typeof(Repository<,>).Assembly;
            var repoRegistrations = repoAssembly.GetExportedTypes()
                .Where(t => t.IsAbstract == false && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,>)))
                .Select( t => new {Type = t, Service = t.GetInterfaces().Where(i => i.IsGenericType == false).First()});

            foreach (var reg in repoRegistrations)
            {
                _container.Register(reg.Type, reg.Type, Lifestyle.Scoped);
                _container.Register(reg.Service, () => _container.GetInstance(reg.Type));
            }

        }







    }
}
