using MacroContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MacroContext.Infrastructure.Abstractions.MetaData
{
    [DebuggerDisplay("{QueryType.Name,nq}")]
    public sealed class QueryInfo
    {
        public readonly Type QueryType;
        public readonly Type ResultType;

        public QueryInfo(Type queryType)
        {
            this.QueryType = queryType;
            this.ResultType = DetermineResultTypes(queryType).Single();
        }

        public static bool IsQuery(Type type) => DetermineResultTypes(type).Any();

        private static IEnumerable<Type> DetermineResultTypes(Type type) =>
            from interfaceType in type.GetInterfaces()
            where interfaceType.IsGenericType
            where interfaceType.GetGenericTypeDefinition() == typeof(IQuery<>)
            select interfaceType.GetGenericArguments()[0];
    }
}