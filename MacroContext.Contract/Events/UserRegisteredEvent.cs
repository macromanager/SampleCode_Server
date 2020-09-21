using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Events
{
    public class UserRegisteredEvent : IEvent
    {
        public UserRegisteredEvent(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
