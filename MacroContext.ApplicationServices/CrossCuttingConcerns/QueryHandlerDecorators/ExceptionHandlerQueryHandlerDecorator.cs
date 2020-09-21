using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.ApplicationServices.QueryHandlers;
using MacroContext.Contract.Queries;
using MacroContext.Persistance;
using MacroContext.Shared.Utilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.ApplicationServices.CrossCuttingConcerns.QueryHandlerDecorators
{
    public class ExceptionHandlerQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery,TResult> //: ICommandHandler<TCommand>
        where TQuery : IQuery<TResult>
    {
        private IQueryHandler<TQuery, TResult> _decorated;
        private ILogger _logger;

        public ExceptionHandlerQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> decorated, 
            ILogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }

        public TResult Handle(TQuery query)
        {
            try
            {
                return _decorated.Handle(query);
            }
            catch(Exception e)
            {
                _logger.Log("Error occured during query: " + query.GetType().Name, e);
                throw;
            }
        }
    }
}
