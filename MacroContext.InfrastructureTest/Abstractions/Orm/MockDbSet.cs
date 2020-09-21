using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace MacroContext.InfrastructureTest.Abstractions.Orm
{
    public static class MockDbSet
    {
        public static DbSet<T> GetFakeDbSet<T>(this IQueryable<T> queryable) where T : class
        {
            DbSet<T> fakeDbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
            ((IQueryable<T>)fakeDbSet).Provider.Returns(queryable.Provider);
            ((IQueryable<T>)fakeDbSet).Expression.Returns(queryable.Expression);
            ((IQueryable<T>)fakeDbSet).ElementType.Returns(queryable.ElementType);
            ((IQueryable<T>)fakeDbSet).GetEnumerator().Returns(queryable.GetEnumerator());
            //fakeDbSet.AsNoTracking().Returns(fakeDbSet);
            return fakeDbSet;
        }
    }
}
