using MacroContext.Contract;
using MacroContext.Infrastructure.MessengerServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.Abstractions
{
    public class RequestOrchestrator
    {
        private ServiceHosts _hosts;      
        
        public RequestOrchestrator(ServiceHosts hosts)
        {
            _hosts = hosts;
        } 
        public ConnectionInfo GetConnectionInfo()
        {

            var cmdUri = _hosts.CommandProcessorServiceHost.Description.Endpoints.Where(pt => pt.Name == "mex").First().Address;
            var queryUri = _hosts.QueryProcessorServiceHost.Description.Endpoints.Where(pt => pt.Name == "mex").First().Address;
            var eventUri = ConfigurationManager.ConnectionStrings["RabbitMQ"].ConnectionString;
            var exchangeName = ConfigurationManager.ConnectionStrings["RabbitMQExchangeName"].ConnectionString;

            var info = new ConnectionInfo(cmdUri.ToString(), queryUri.ToString(), eventUri, exchangeName);
            return info;
        }

    }
}
