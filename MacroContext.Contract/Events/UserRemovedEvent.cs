using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Events
{
    public class UserRemovedEvent : IEvent
    {
        public UserRemovedEvent(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
