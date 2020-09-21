using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Errors
{
    public class EntitiesAlreadyExistsError
    {
        public EntitiesAlreadyExistsError(Guid[] entityIds)
        {
            this.EntityIds = entityIds;
        }
        public Guid[] EntityIds { get; set; }
    }
}
