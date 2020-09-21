using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.ApplicationServices.InternalExceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(Guid[] entityIds)
        {
            this.EntityIds = entityIds;
        }
        public Guid[] EntityIds { get; private set; }
    }
}
