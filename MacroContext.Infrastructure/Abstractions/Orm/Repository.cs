using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MacroContext.Domain;
using MacroContext.Persistance;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Infrastructure.Abstractions.Orm
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {

        protected IMacroContextDb _context;

        public void SetContext(IMacroContextDb context)
        {
            _context = context;
        }


        public virtual TEntity Get(TId Id)
        {
            return _context.Set<TEntity>().Find(Id);
        }


        public virtual PagedResult<TEntity> GetAll(PagingInformation pagingInfo)
        {
            var paged = ApplyPaging(_context.Set<TEntity>(), pagingInfo);
            return paged;
        }

        protected abstract IOrderedQueryable<TEntity> SortCollectionBy(IQueryable<TEntity> query);


        protected virtual PagedResult<TEntity> ApplyPaging(IQueryable<TEntity> query, PagingInformation pagingInfo)
        {
            int count = query.Count();
            var pageSize = pagingInfo.PageSize;
            var pageIndex = pagingInfo.PageIndex;
            var ordered = this.SortCollectionBy(query);
            var page = ordered.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToArray();
            return new PagedResult<TEntity>(
                paging: pagingInfo,
                pageCount: (count + (pageSize - 1)) / pageSize,
                itemCount: count,
                result: page);
        }

        //protected virtual IEnumerable<TEntity> GetPagedResults(IQueryable<TEntity> query, int pageSize, int pageIndex)
        //{
        //    int count = query.Count();
        //    var page = query.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        //    return page;
        //}

        public virtual void Update(TEntity entity)
        {
            _context.SetEntityState(entity, EntityState.Modified);
        }

        public virtual void Add(TEntity Entity)
        {
            _context.Set<TEntity>().Add(Entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> Entities)
        {
            _context.Set<TEntity>().AddRange(Entities);
        }

        public virtual IEnumerable<TEntity> FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _context.Set<TEntity>().Where(predicate);
            return query.ToList();
        }

        public virtual PagedResult<TEntity> Find(Expression<Func<TEntity, bool>> predicate, PagingInformation pagingInfo)
        {
            var query = _context.Set<TEntity>().Where(predicate);
            var paged = this.ApplyPaging(query, pagingInfo);
            return paged;
        }


        public virtual void Remove(TEntity entity)
        {
            _context.SetEntityState(entity, EntityState.Deleted);
            //var entry = _context.Entry(entity);
            //entry.State = EntityState.Deleted;

        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        public virtual void Attach(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public virtual void Detach(TEntity Entity)
        {
            _context.SetEntityState(Entity, EntityState.Detached);
            //_context.Entry<TEntity>(Entity).State = EntityState.Detached;
        }
    }
}
