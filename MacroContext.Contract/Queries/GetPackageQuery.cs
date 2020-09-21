using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Dto;

namespace MacroContext.Contract.Queries
{
    public class GetPackageQuery : IQuery<PackageDto>
    {
        public GetPackageQuery() { }
        public GetPackageQuery(Guid packageId)
        {
            PackageId = packageId;
        }
        public Guid PackageId { get; set; }
    }
}
