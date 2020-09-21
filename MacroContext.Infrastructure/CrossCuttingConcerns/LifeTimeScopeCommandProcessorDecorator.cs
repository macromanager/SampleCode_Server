using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Commands;
using SimpleInjector;
using MacroContext.ApplicationServices.CommandHandlers;

namespace MacroContext.Infrastructure.CrossCuttingConcerns
{
    public class LifeTimeScopeCommandProcessorDecorator : ICommandProcessor
    {
        private Func<ICommandProcessor> _decoratedFactory;
        public LifeTimeScopeCommandProcessorDecorator(Func<ICommandProcessor> decoratedFactory)
        {
            _decoratedFactory = decoratedFactory;
        }


        public void SubmitBatch(params ICommand[] commands)
        {
            using (AsyncScopedLifestyle.BeginScope(Bootstrapper.Container))
            {
                var decorated = _decoratedFactory.Invoke();
                decorated.SubmitBatch(commands);
            }

        }
    }
}
