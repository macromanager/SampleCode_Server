using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.ApplicationServices.QueryHandlers;
using MacroContext.Contract.Queries;
using MacroContext.Infrastructure.CrossCuttingConcerns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.Abstractions
{
    public class QueryProcessor : IQueryProcessor
    {

        public TResult SubmitBatch<TResult>(IQuery<TResult> query)
        {
            return Submit(query);
        }

        private TResult Submit<TResult>(IQuery<TResult> query)
        {
            var resultType = typeof(TResult);
            Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), resultType);
            dynamic queryHandler = Bootstrapper.Container.GetInstance(handlerType);
            var result = queryHandler.Handle((dynamic)query);
            return result;


        }
    }
}
