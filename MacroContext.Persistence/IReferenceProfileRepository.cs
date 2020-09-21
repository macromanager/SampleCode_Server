using MacroContext.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Persistance
{
    public interface IReferenceProfileRepository : IRepository<ReferenceProfile,Guid>
    {
        IEnumerable<ReferenceProfile> GetByPackageId(Guid packageId);
    }
}
