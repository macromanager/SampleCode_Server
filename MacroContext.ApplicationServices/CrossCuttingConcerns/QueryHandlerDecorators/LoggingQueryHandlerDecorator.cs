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
    public class LoggingQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery,TResult> //: ICommandHandler<TCommand>
        where TQuery : IQuery<TResult>
    {
        private IQueryHandler<TQuery, TResult> _decorated;
        private ILogger _logger;

        public LoggingQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> decorated, 
            ILogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }

        public TResult Handle(TQuery query)
        {
            var errorOccured = false;
            var queryName = query.GetType().Name;
            try
            {
                _logger.Log("Started Query: " + queryName);
                return _decorated.Handle(query);
            }
            catch
            {
                errorOccured = true;
                throw;
            }
            finally
            {
                if (!errorOccured)
                {
                    _logger.Log("Ended Query: " + queryName);
                }
            }
        }
    }
}
