using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Domain;
using System.Data.Entity;
using MacroContext.Persistance;

namespace MacroContext.Infrastructure.Abstractions.Orm
{
    public class MacroProfileRepository : Repository<MacroProfile, Guid>, IMacroProfileRepository
    {
        public MacroProfileRepository()//(MacroContextDb context) : base(context)
        {    
        }

        public IEnumerable<MacroProfile> GetByPackageId(Guid packageId)
        {
            return _context.Set<MacroProfile>()
                .Include(p => p.Macro)
                .Where(p => p.PackageId == packageId);
        }

        protected override IOrderedQueryable<MacroProfile> SortCollectionBy(IQueryable<MacroProfile> query)
        {
            return query.OrderBy(profile => profile.ComponentName);
        }
    }
}
