using MacroContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Queries
{
    public class GetPackageLibraryMetadata : IQuery<PackageLibraryMetadata>
    {
        public GetPackageLibraryMetadata(Guid userId = default(Guid))
        {
            this.UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
