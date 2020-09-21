using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Contract.Commands;
using MacroContext.Infrastructure.CrossCuttingConcerns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.Abstractions
{
    public class CommandProcessor : ICommandProcessor
    {
        private ICommandDispatcher _dispatcher;

        public CommandProcessor(ICommandDispatcher dispatcher)
        {
            this._dispatcher = dispatcher;
        }

        public void SubmitBatch(params ICommand[] commads)
        {
            foreach (var command in commads)
            {
                this._dispatcher.Submit((dynamic)command);
            }
        }

    }
}
