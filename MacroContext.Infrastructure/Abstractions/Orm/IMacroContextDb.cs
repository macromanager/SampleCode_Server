using MacroContext.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.Abstractions.Orm
{
    public interface IMacroContextDb : IDisposable
    {
        DbSet<Macro> Macros { get; set; }
        DbSet<Package> Packages { get; set; }
        DbSet<MacroProfile> MacroProfiles { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<ReferenceProfile> ReferenceProfiles { get; set; }
        void SetModifiedProperty<TEntity, TProperty>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, TProperty>> property, bool isModified) where TEntity : class, IEntity<Guid>;
        DbSet<TEntity> Set<TEntity>() where TEntity: class;
        //DbEntityEntry Entry(object entity);
        //DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void SetEntityState(object enttiy, EntityState state);

        int SaveChanges();

    }
}
