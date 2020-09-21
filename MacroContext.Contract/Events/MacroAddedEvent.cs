using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Events
{
    public class MacroAddedEvent : IEvent
    {
        public MacroAddedEvent(Guid macroId, Guid macroProfileId)
        {
            MacroId = macroId;
            MacroProfileId = macroProfileId;
        }

        public Guid MacroId { get; set; }
        public Guid MacroProfileId { get; set; }
    }
}
