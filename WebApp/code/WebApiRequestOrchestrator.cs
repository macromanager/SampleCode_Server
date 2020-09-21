using WebApp;
using MacroContext.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using MacroContext.Infrastructure;

namespace WebApp.code
{
    public class WebApiRequestOrchestrator
    {
        public static string CommandRouteApi;
        public static string QueryRouteApi;
        public static string ConnectionsRouteApi;
        public static string EventPublisherUrl;
        public static string ExchangeName;

        static WebApiRequestOrchestrator()
        {
            ConnectionsRouteApi = ConfigurationManager.ConnectionStrings["ConnectionsRouteApi"].ConnectionString;
            CommandRouteApi = ConfigurationManager.ConnectionStrings["CommandRouteApi"].ConnectionString;
            QueryRouteApi = ConfigurationManager.ConnectionStrings["QueryRouteApi"].ConnectionString;
            EventPublisherUrl = AppSettings.RABBITMQ_CONNECTION_STRING;
            ExchangeName = AppSettings.RABBITMQ_EXCHANGE_NAME;
        }

        public ConnectionInfo GetConnectionInfo()
        {
            var baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            var cmdUrl = baseUrl + "/" + CommandRouteApi;
            var queryUrl = baseUrl + "/" + QueryRouteApi;
            var eventUri = EventPublisherUrl;
            var exchangeName = ExchangeName;
            var info = new ConnectionInfo(cmdUrl, queryUrl, eventUri, exchangeName);
            return info;
        }
    }
}
