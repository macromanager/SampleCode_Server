using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Events
{
    public class ReferenceProfileAddedEvent : IEvent
    {
        public ReferenceProfileAddedEvent(Guid referenceProfileId)
        {
            this.ReferenceProfileId = referenceProfileId;
        }

        public Guid ReferenceProfileId { get; set; }
    }
}
