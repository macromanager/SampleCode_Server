using MacroContext.Contract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.CrossCuttingConcerns
{
    public interface lEventPublisher
    {
        void Publish(TransactionResult eventResult); //where TEvent : IEvent;
    }
}
