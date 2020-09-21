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
    public class ReferenceProfileRepository : Repository<ReferenceProfile,Guid>, IReferenceProfileRepository
    {
        public ReferenceProfileRepository()
        {
        }

        public IEnumerable<ReferenceProfile> GetByPackageId(Guid packageId)
        {
            return _context.Set<ReferenceProfile>()
                //.Include(p => p.)
                .Where(p => p.PackageId == packageId);
        }

        protected override IOrderedQueryable<ReferenceProfile> SortCollectionBy(IQueryable<ReferenceProfile> query)
        {
            return query.OrderBy(reference => reference.Name);

        }
    }
}
