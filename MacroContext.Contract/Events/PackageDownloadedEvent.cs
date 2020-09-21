using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Events
{
    public class PackageDownloadedEvent : IEvent
    {
        public PackageDownloadedEvent(Guid packageId)
        {
            PackageId = packageId;
        }
        public Guid PackageId { get; private set; }
    }
}
