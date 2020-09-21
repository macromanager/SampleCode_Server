using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Commands;
using SimpleInjector;
using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Contract.Queries;

namespace MacroContext.Infrastructure.CrossCuttingConcerns
{
    public class LifeTimeScopeQueryProcessorDecorator : IQueryProcessor
    {
        private Func<IQueryProcessor> _decoratedFactory;
        public LifeTimeScopeQueryProcessorDecorator(Func<IQueryProcessor> decoratedFactory)
        {
            _decoratedFactory = decoratedFactory;
        }

        public TResult SubmitBatch<TResult>(IQuery<TResult> query)
        {
            using (AsyncScopedLifestyle.BeginScope(Bootstrapper.Container))
            {
                var decorated = _decoratedFactory.Invoke();
                return decorated.SubmitBatch(query);
            }

        }
    }
}
