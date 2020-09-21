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
using MacroContext.Contract.Queries;
using MacroContext.ApplicationServices.QueryHandlers;
using MacroContext.Infrastructure.Abstractions;
using MacroContext.Infrastructure.CrossCuttingConcerns;

namespace MacroContext.Infrastructure.MessengerServices
{
    public class QueryProcessorService : IQueryProcessorService
    {
        public QueryProcessorService() { } // parameterless constructor required for wcf


        public string Submit(string typeName, string json)
        {
            try
            {
                Type queryType = typeof(IQuery<>).Assembly.GetType(typeName);
                Type returnType = queryType.GetInterfaces()[0].GetGenericArguments()[0];
                dynamic query = JsonConvert.DeserializeObject(json, queryType);
                var processor = Bootstrapper.Container.GetInstance<IQueryProcessor>();
                var result = processor.SubmitBatch(query);
                var jsonResult = JsonConvert.SerializeObject(result);
                return jsonResult;
            }

            catch(Exception e)
            {
                Console.Write(e.Message);
                throw;
            }

        }

    }
}
