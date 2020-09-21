using MacroContext.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.ApplicationServices.CommandHandlers
{
    public class BatchCommandHandler : ICommandHandler<BatchCommand>
    {
        private ICommandDispatcher _dispatcher;

        public BatchCommandHandler(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        public void Execute(BatchCommand command)
        {
            foreach (var cmd in command.Commands)
            {
                _dispatcher.Submit((dynamic)cmd);

            }

        }
    }
}
