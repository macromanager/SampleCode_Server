using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Contract;
using MacroContext.Contract.Queries;
using MacroContext.Infrastructure;
using MacroContext.Shared.ValueObjects;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WebApp.code;

namespace WebApp.AppStart
{
    public static class Initializer
    {
        public static void Initialize()
        {

            // //container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            //AreaRegistration.RegisterAllAreas();
            Bootstrapper.RegisterServices();
            var container = Bootstrapper.Container;
            Register(container);
            Bootstrapper.VerifyContainer();
            WebApiConfig.Register(GlobalConfiguration.Configuration, container);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static void Register(Container container)
        {
            container.Register(typeof(WebApiRequestOrchestrator));

        }
    }

   
}