using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.CommandHandlers;
using MacroContext.Contract.Events;

namespace MacroContext.Infrastructure.CrossCuttingConcerns
{
    public class EventStoreImpl : IEventStore
    {
        private List<IEvent> _events;

        public EventStoreImpl()
        {
            _events = new List<IEvent>();
        }

        public void AddToEventQueue(IEvent e)
        {
            _events.Add(e);
        }



        public IEvent[] GetEventQueue()
        {
            return _events.ToArray();
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
