using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using System.Reflection;
using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Contract;
using MacroContext.Contract.Commands;
using Newtonsoft.Json;
using MacroContext.Infrastructure.Abstractions;
using MacroContext.Infrastructure.CrossCuttingConcerns;

namespace MacroContext.Infrastructure.MessengerServices
{
    public class CommandProcessorService : ICommandProcessorService
    {
        public CommandProcessorService() { } // parameterless constructor required for wcf

        public void Submit(string typeName, string json)
        {
            try
            {
                Type commandType = typeof(ICommand).Assembly.GetType(typeName);
                var jsonSerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
                dynamic command = JsonConvert.DeserializeObject(json, commandType, jsonSerializerSettings);
                var processor = Bootstrapper.Container.GetInstance<ICommandProcessor>();
                processor.SubmitBatch(command);
            }

            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            }


        }

    }
}
