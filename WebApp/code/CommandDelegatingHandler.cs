using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Contract;
using MacroContext.Infrastructure;
using MacroContext.Infrastructure.MessengerServices;
using MacroContext.Shared.Utilities.Logger;
using Newtonsoft.Json;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.code
{
    public sealed class CommandDelegatingHandler : DelegatingHandler
    {
        private readonly Dictionary<string, Type> commandTypes;
        private readonly ILogger logger = Bootstrapper.Container.GetInstance<ILogger>();

        //private readonly ICommandProcessor commandProcessor;

        public CommandDelegatingHandler(IEnumerable<Type> commandTypes)
        {
            //this.commandProcessor = commandProcessor;
            this.commandTypes = commandTypes.ToDictionary(
                keySelector: type => type.Name,//.Replace("Command", string.Empty),
                elementSelector: type => type,
                comparer: StringComparer.OrdinalIgnoreCase);

        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string commandName = request.GetRouteData().Values["command"].ToString();

            logger.Log("Recieved Command: " + commandName);

            if (request.Method != HttpMethod.Post)
            {
                return request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed,
                    "The requested resource does not support http method '" + request.Method + "'.");
            }

            if (!this.commandTypes.ContainsKey(commandName))
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound, RequestMessage = request };
            }

            Type commandType = this.commandTypes[commandName];

            string requestData = await request.Content.ReadAsStringAsync();

            Type handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);

            // GetDependencyScope() calls IDependencyResolver.BeginScope internally.
            request.GetDependencyScope();
           
            this.ApplyHeaders(request);

            //dynamic handler = this.handlerFactory.Invoke(handlerType);

            try
            {
                var logger = Bootstrapper.Container.GetInstance<ILogger>();

                using (AsyncScopedLifestyle.BeginScope(Bootstrapper.Container))
                {
                    //var commandVessel = DeserializeCommandVessel(request, requestData);
                    var routingKey = request.Headers.GetValueOrNull("eventRoutingKey");
                    var eventPublisher = Bootstrapper.Container.GetInstance<EventPublisher>();
                    eventPublisher.RoutingKey = routingKey;

                    dynamic command = DeserializeCommand(request, requestData, commandType);
                    var commandProcessor = Bootstrapper.Container.GetInstance<ICommandProcessor>();
                    commandProcessor.SubmitBatch(command);
                    //handler.Handle(command);

                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, RequestMessage = request };
                }
                
            }
            catch (Exception ex)
            {
                var response = WebApiErrorResponseBuilder.CreateErrorResponseOrNull(ex, request);

                if (response != null)
                {
                    logger.Log("Error Translated", ex);
                    return response;
                }
                logger.Log(LoggingEventType.Fatal, "Unknown Error", ex);
                throw;
            }
        }

        private void ApplyHeaders(HttpRequestMessage request)
        {
            // TODO: Here you read the relevant headers and and check them or apply them to the current scope
            // so the values are accessible during execution of the command.
            string sessionId = request.Headers.GetValueOrNull("sessionId");
            string token = request.Headers.GetValueOrNull("CSRF-token");
        }

        //private static CommandDataVessel DeserializeCommandVessel(HttpRequestMessage request, string json)
        //{
        //    var vessel = JsonConvert.DeserializeObject<CommandDataVessel>(json, GetJsonFormatter(request).SerializerSettings);
        //    return vessel;
        //}


        private static object DeserializeCommand(HttpRequestMessage request, string json, Type commandType)
        {
            //var jsonSerializerSettings = GetJsonFormatter(request).SerializerSettings;
            var jsonSerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            var command = JsonConvert.DeserializeObject(json, commandType, jsonSerializerSettings);
            return command;

        }

        private static JsonMediaTypeFormatter GetJsonFormatter(HttpRequestMessage request) =>
            request.GetConfiguration().Formatters.JsonFormatter;
    }

}