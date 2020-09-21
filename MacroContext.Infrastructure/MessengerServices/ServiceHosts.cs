using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.MessengerServices
{
    public class ServiceHosts
    {
        private ServiceHost _cmdProcessorHost;
        private ServiceHost _queryProcessorHost;
        private ServiceHost _connectionServiceHost;

        public ServiceHosts()
        {
            _connectionServiceHost = new ServiceHost(typeof(ClientConnectionService));
            _cmdProcessorHost = new ServiceHost(typeof(CommandProcessorService));
            _queryProcessorHost = new ServiceHost(typeof(QueryProcessorService));

        }

        public ServiceHost CommandProcessorServiceHost { get { return _cmdProcessorHost; } }
        public ServiceHost QueryProcessorServiceHost { get { return _queryProcessorHost; } }
        public ServiceHost ClientConnectionServiceHost { get { return _connectionServiceHost; } }

        public void OpenServices()
        {
            _cmdProcessorHost.Open();
            _connectionServiceHost.Open();
            _queryProcessorHost.Open();
        }

        public void CloseServices()
        {
            if (_cmdProcessorHost != null) { _cmdProcessorHost.Close(); }
            if(_connectionServiceHost != null) { _connectionServiceHost.Close(); }
            if (_queryProcessorHost != null){ _queryProcessorHost.Close(); }
            _cmdProcessorHost = null;
            _connectionServiceHost = null;
            _queryProcessorHost = null;
        }


    }
}
