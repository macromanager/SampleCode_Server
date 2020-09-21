using MacroContext.ApplicationServices.QueryHandlers;
using MacroContext.Contract;
using MacroContext.Infrastructure;
using MacroContext.Infrastructure.Abstractions;
using MacroContext.Infrastructure.Abstractions.MetaData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace WebApp.code
{
    public sealed class ClientConnectionDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            Console.WriteLine("test connection request recieved");
            System.Diagnostics.Trace.WriteLine("test my trace logger");

            // GET operations get their data through the query string, while POST operations expect a JSON
            // object being put in the body.
            string queryData = request.Method == HttpMethod.Get
                ? SerializationHelpers.ConvertQueryStringToJson(request.RequestUri.Query)
                : await request.Content.ReadAsStringAsync();            

            // GetDependencyScope() calls IDependencyResolver.BeginScope internally.
            request.GetDependencyScope();

            this.ApplyHeaders(request);            

            try
            {

                var _requestOrchestrator = Bootstrapper.Container.GetInstance<WebApiRequestOrchestrator>();
                var orchestrator = _requestOrchestrator;
                var result = orchestrator.GetConnectionInfo();

                return CreateResponse(result, typeof(ConnectionInfo), HttpStatusCode.OK, request);
            }
            catch (Exception ex)
            {
                var response = WebApiErrorResponseBuilder.CreateErrorResponseOrNull(ex, request);

                if (response != null)
                {
                    return response;
                }

                throw;
            }
        }

        private void ApplyHeaders(HttpRequestMessage request)
        {
            // TODO: Here you read the relevant headers and check them or apply them to the current scope
            // so the values are accessible during execution of the query.
            string sessionId = request.Headers.GetValueOrNull("sessionId");
            string token = request.Headers.GetValueOrNull("CSRF-token");
        }

        private static HttpResponseMessage CreateResponse(object data, Type dataType, HttpStatusCode code,
            HttpRequestMessage request)
        {
            var configuration = request.GetConfiguration();

            IContentNegotiator negotiator = configuration.Services.GetContentNegotiator();
            ContentNegotiationResult result = negotiator.Negotiate(dataType, request, configuration.Formatters);

            var bestMatchFormatter = result.Formatter;
            var mediaType = result.MediaType.MediaType;

            return new HttpResponseMessage
            {
                Content = new ObjectContent(dataType, data, bestMatchFormatter, mediaType),
                StatusCode = code,
                RequestMessage = request
            };
        }

        private static dynamic DeserializeQuery(HttpRequestMessage request, string json, Type queryType) =>
            JsonConvert.DeserializeObject(json, queryType, GetJsonFormatter(request).SerializerSettings);

        private static JsonMediaTypeFormatter GetJsonFormatter(HttpRequestMessage request) =>
            request.GetConfiguration().Formatters.JsonFormatter;
    }
}