using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Infrastructure;
using MacroContext.Infrastructure.Abstractions.MetaData;
using Newtonsoft.Json.Serialization;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.code;

namespace WebApp.AppStart
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, Container container)
        {
            UseCamelCaseJsonSerialization(config);

            if (AppSettings.IsDebugMode)
            {
                UseIndentJsonSerialization(config);
            }

            MapRoutes(config, container);
        }

        private static void MapRoutes(HttpConfiguration config, Container container)
        {

            config.Routes.MapHttpRoute(
               name: "Index",
               routeTemplate: "{id}.html",
               defaults: new { id = "index" });

            config.Routes.MapHttpRoute(
                name: "ConnectionApi",
                routeTemplate: WebApiRequestOrchestrator.ConnectionsRouteApi,
                defaults: new { },
                constraints: new { },
                handler: new ClientConnectionDelegatingHandler()
                );

            config.Routes.MapHttpRoute(
                name: "QueryApi",
                routeTemplate: WebApiRequestOrchestrator.QueryRouteApi+ "/{query}",
                defaults: new { },
                constraints: new { },
                handler: new QueryDelegatingHandler(
                    queryTypes: MessageTypeManager.GetQueryTypes()));

            config.Routes.MapHttpRoute(
                name: "CommandApi",
                routeTemplate: WebApiRequestOrchestrator.CommandRouteApi + "/{command}",
                defaults: new { },
                constraints: new { },
                handler: new CommandDelegatingHandler(
                    commandTypes: MessageTypeManager.GetCommandTypes()));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }

        private static void UseCamelCaseJsonSerialization(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }

        private static void UseIndentJsonSerialization(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.Indent = true;
        }
    }
}