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
    public class MacroRepository : Repository<Macro,Guid>, IMacroRepository
    {
        public MacroRepository()
        {
        }

        protected override IOrderedQueryable<Macro> SortCollectionBy(IQueryable<Macro> query)
        {
            return query.OrderBy(macro => macro.Name);
        }

        //public Macro GetWithDetails(Guid id)
        //{
        //    return _context.Set<Macro>()
        //        .Include(pt => pt.ContactInfo)
        //        .Include(pt => pt.Identification)
        //        .Where(pt => pt.Id == id)
        //        .FirstOrDefault();
        //}

    }
}
