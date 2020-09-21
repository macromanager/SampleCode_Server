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
    public class UserRepository : Repository<User,Guid>, IUserRepository
    {
        public UserRepository()
        {
        }

        protected override IOrderedQueryable<User> SortCollectionBy(IQueryable<User> query)
        {
            return query.OrderBy(pkg => pkg.Name);
        }
    }
}
