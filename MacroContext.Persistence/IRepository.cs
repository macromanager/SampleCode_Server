using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MacroContext.Domain;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Persistance
{
    public interface IRepository<TEntity,TId> where TEntity : class, IEntity<TId>
    {
        TEntity Get(TId Id);
        PagedResult<TEntity> GetAll(PagingInformation pagingInfo);
        PagedResult<TEntity> Find(Expression<Func<TEntity, bool>> predicate, PagingInformation pagingInfo);
        IEnumerable<TEntity> FindOne(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity Entity);
        void AddRange(IEnumerable<TEntity> Entities);
        void Remove(TEntity Entity);
        void RemoveRange(IEnumerable<TEntity> Entities);
        void Update(TEntity Entity);
        void Attach(TEntity Entity);
        void Detach(TEntity Entity);



    }
}
