using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Contract.Commands;
using MacroContext.Persistance;
using MacroContext.Shared.Utilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.ApplicationServices.CrossCuttingConcerns.CommandHandlerDecorators
{
    public class ExceptionHandlerCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private ICommandHandler<TCommand> _decorated;
        private ILogger _logger;

        public ExceptionHandlerCommandHandlerDecorator(
            ICommandHandler<TCommand> decorated, 
            ILogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }
        public void Execute(TCommand command)
        {
            try
            {
                _decorated.Execute(command);
            }
            catch(Exception e)
            {
                _logger.Log("Error occured during command: " + command.GetType().Name, e);
                throw;
            }
        }
    }
}
