using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Events
{
    public class TransactionResult //<TEvent> where TEvent: IEvent
    {
        public TransactionResult(Guid commandId, IEvent[] e, bool succeeded, string error)
        {
            CommandId = commandId;
            Events = e;
            Succeeded = succeeded;
            Error = error;
        }
        public Guid CommandId { get; set; }
        public IEvent[] Events { get; set; }
        public bool Succeeded { get; private set; } 
        public string Error { get; private set; }
    }
}
