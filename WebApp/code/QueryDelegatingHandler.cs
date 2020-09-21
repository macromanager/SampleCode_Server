using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.ApplicationServices.QueryHandlers;
using MacroContext.Infrastructure;
using MacroContext.Infrastructure.Abstractions.MetaData;
using MacroContext.Shared.Utilities.Logger;
using Newtonsoft.Json;
using SimpleInjector.Lifestyles;
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
    public sealed class QueryDelegatingHandler : DelegatingHandler
    {
        private readonly Dictionary<string, QueryInfo> queryTypes;
        private readonly ILogger logger = Bootstrapper.Container.GetInstance<ILogger>();

        public QueryDelegatingHandler(IEnumerable<QueryInfo> queryTypes)
        {
            this.queryTypes = queryTypes.ToDictionary(
                keySelector: info => info.QueryType.Name.Replace("Query", string.Empty),
                elementSelector: info => info,
                comparer: StringComparer.OrdinalIgnoreCase);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            System.Diagnostics.Trace.WriteLine("test my trace request logger");
            string rawQueryName = request.GetRouteData().Values["query"].ToString();
            var queryName = rawQueryName.Replace("Query", string.Empty);

            logger.Log("Recieved Query: " + queryName);


            if (!this.queryTypes.ContainsKey(queryName))
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound, RequestMessage = request };
            }

            // GET operations get their data through the query string, while POST operations expect a JSON
            // object being put in the body.
            string queryData = request.Method == HttpMethod.Get
                ? SerializationHelpers.ConvertQueryStringToJson(request.RequestUri.Query)
                : await request.Content.ReadAsStringAsync();

            QueryInfo info = this.queryTypes[queryName];

            Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(info.QueryType, info.ResultType);

            // GetDependencyScope() calls IDependencyResolver.BeginScope internally.
            request.GetDependencyScope();

            this.ApplyHeaders(request);

            try
            {

                using (AsyncScopedLifestyle.BeginScope(Bootstrapper.Container))
                {
                    var queryProcessor = Bootstrapper.Container.GetInstance<IQueryProcessor>();
                    dynamic query = DeserializeQuery(request, queryData, info.QueryType);
                    var result = queryProcessor.SubmitBatch(query);

                    return CreateResponse(result, info.ResultType, HttpStatusCode.OK, request);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                System.Diagnostics.Trace.WriteLine("test my trace exception logger");
                System.Diagnostics.Trace.WriteLine(ex.Message);

                System.Diagnostics.Trace.WriteLine(ex);

                Console.WriteLine(ex);

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