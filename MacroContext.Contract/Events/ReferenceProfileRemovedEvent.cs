using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Events
{
    public class ReferenceProfileRemovedEvent : IEvent
    {
        public ReferenceProfileRemovedEvent(Guid referenceProfileId)
        {
            ReferenceProfileId = referenceProfileId;
        }
        public Guid ReferenceProfileId { get; set; }
    }
}
