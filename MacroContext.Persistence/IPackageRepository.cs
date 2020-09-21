using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Domain;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Persistance
{
    public interface IPackageRepository : IRepository<Package,Guid>
    {
        PagedResult<Package> GetPackagesByUserId(Guid userId, PagingInformation paging);
        void BunpPackageDownloads(Guid packageId);

        PackageLibraryMetadata GetPackageLibraryMetadata(Guid userId = default(Guid));

    }
}
