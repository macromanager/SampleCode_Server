using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Commands;
using MacroContext.Infrastructure.Abstractions.Orm;
using MacroContext.ApplicationServices.CommandHandlers;

namespace MacroContext.Infrastructure.CrossCuttingConcerns
{
    public class TransactionScopeCommandProcessorDecorator : ICommandProcessor
    {
        private ICommandProcessor _decorated;
        private MacroContextDb _context;

        public TransactionScopeCommandProcessorDecorator(ICommandProcessor decorated, MacroContextDb context)
        {
            _decorated = decorated;
            _context = context;
        }


        public void SubmitBatch(params ICommand[] commands)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                _decorated.SubmitBatch(commands);
                transaction.Commit();
            }
        }
    }
}
