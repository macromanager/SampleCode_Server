using MacroContext.Contract;
using MacroContext.Infrastructure.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.MessengerServices
{
    public class ClientConnectionService : IClientConnectionService
    {
        public ClientConnectionService() { } // parameterless constructor required for wcf

        public string Connect()
        {

            try
            {
                var orchestrator = Bootstrapper.Container.GetInstance<RequestOrchestrator>();
                var info = orchestrator.GetConnectionInfo();
                var jsonSerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
                var json = JsonConvert.SerializeObject(info, jsonSerializerSettings);
                return json;

            }

            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            }


        }

    }
}
