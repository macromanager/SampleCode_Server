using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract
{
    public class ConnectionInfo
    {
        public ConnectionInfo(string commandServiceUri, string queryServiceUri, string eventPublisherServiceUri, string exchangeName)
        {
            this.CommanndServiceUri = commandServiceUri;
            this.QueryServiceUri = queryServiceUri;
            this.EventPublisherServiceUri = eventPublisherServiceUri;
            this.ExchangeName = exchangeName;            
        }
        public string CommanndServiceUri { get; set; }
        public string QueryServiceUri { get; set; }
        public string EventPublisherServiceUri { get; set; }
        public string ExchangeName { get; set; }
        //public AmqpInfo AmqpInfo { get; set; }
    }
}
