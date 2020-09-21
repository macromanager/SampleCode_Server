using MacroContext.Contract.Commands;
using MacroContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MacroContext.Infrastructure.Abstractions.MetaData
{
    public static class MessageTypeManager
    {
        private static Assembly contractAssembly;

        static MessageTypeManager()
        {
            contractAssembly = typeof(ICommand).Assembly;
        }

        public static IEnumerable<Type> GetCommandTypes()
        {
            var commandType = typeof(ICommand);
            var contractAssembly = typeof(ICommand).Assembly;
            var types = contractAssembly.GetExportedTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Contains(commandType));

            return types;
        }
           

        public static IEnumerable<QueryInfo> GetQueryTypes()
        {
            var queryType = typeof(IQuery<>);
            var types = contractAssembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && ImplementsGenericInterface(t, queryType) )
                .Select(t => new QueryInfo(t));
            return types;
        }

        public static bool ImplementsGenericInterface(Type t, Type genericTypeDefinition)
        {
            var result = t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericTypeDefinition)
                .Count() > 0;
                return result;
        }
            
    }
}