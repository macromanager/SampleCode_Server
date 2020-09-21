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
    public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private ICommandHandler<TCommand> _decorated;
        private ILogger _logger;
        public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> decorated, ILogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }
        public void Execute(TCommand command)
        {
            try
            {
                var commandName = command.GetType().Name;
                _logger.Log("Started Command: " + commandName);
                _decorated.Execute(command);
                _logger.Log("Ended Command: " + commandName);
            }
            catch
            {
                throw;
            }
        }
    }
}
