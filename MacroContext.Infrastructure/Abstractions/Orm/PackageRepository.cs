using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Domain;
using System.Data.Entity;
using MacroContext.Persistance;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Infrastructure.Abstractions.Orm
{
    public class PackageRepository: Repository<Package,Guid>, IPackageRepository
    {
        public PackageRepository()
        {
        }

        public PackageLibraryMetadata GetPackageLibraryMetadata(Guid userId = default(Guid))
        {
            Func<Package, bool> userLibraryPredicate = p => p.UserId == userId;
            Func<Package, bool> allPackagesPredicate = p => true;
            var predicate = userId == default(Guid) ? allPackagesPredicate : userLibraryPredicate;
            var packages = _context.Set<Package>().Where(predicate).OrderByDescending(p => p.Downloads).ThenBy(p=>p.Name);
            var count = packages.Count();
            var pkgWithmostDonwnloads = packages.FirstOrDefault();
            var pkgName = pkgWithmostDonwnloads == null ? "" : pkgWithmostDonwnloads.Name;
            var result = new PackageLibraryMetadata(count, pkgName);
            return result;
        }

        public PagedResult<Package> GetPackagesByUserId(Guid userId, PagingInformation paging)
        {
            var query = _context.Set<Package>()
                .Where((p) => p.UserId == userId);
            var paged = this.ApplyPaging(query, paging);
            return paged;
        }

        protected override IOrderedQueryable<Package> SortCollectionBy(IQueryable<Package> query)
        {
            return query.OrderByDescending(pkg => pkg.Downloads).ThenBy(pkg=>pkg.Name);
        }

        public void BunpPackageDownloads(Guid packageId)
        {
            var db = _context;
            //var pkg = new Package(packageId);
            var pkg =db.Packages.Where(p => p.Id == packageId).Single();
            pkg.Downloaded();
            db.Packages.Attach(pkg);
            db.SetModifiedProperty(pkg, p => p.Downloads, true);
        }

        public override void Update(Package entity)
        {
            _context.SetEntityState(entity, EntityState.Modified);
            _context.SetModifiedProperty(entity, p => p.Downloads, false); //ignore downloads property
        }

        //public MacroPackage GetWithDetails(Guid id)
        //{
        //    return _context.Set<MacroPackage>()
        //        .Include(pt => pt.ContactInfo)
        //        .Include(pt => pt.Identification)
        //        .Where(pt => pt.Id == id)
        //        .FirstOrDefault();
        //}

    }
}
