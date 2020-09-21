using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Events;

namespace ApplicationServices.CommandHandlers
{
    public interface IEventStore
    {
        void AddToEventQueue(IEvent e);

    }
}
