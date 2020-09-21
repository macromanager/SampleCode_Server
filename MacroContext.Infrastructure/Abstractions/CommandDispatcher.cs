using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.Abstractions
{
    public class CommandDispatcher : ICommandDispatcher
    {
        public void Submit<TCommand>(TCommand command) where TCommand : ICommand
        {
            Type handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));
            dynamic commandHandler = Bootstrapper.Container.GetInstance(handlerType);
            commandHandler.Execute((dynamic)command);
        }
    }
}
