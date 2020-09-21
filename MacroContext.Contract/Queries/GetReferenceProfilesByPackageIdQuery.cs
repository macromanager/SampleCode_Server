using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Dto;

namespace MacroContext.Contract.Queries
{
    public class GetReferenceProfilesByPackageIdQuery : IQuery<ReferenceProfileDto[]>
    {
        public GetReferenceProfilesByPackageIdQuery(Guid packageId)
        {
            this.PackageId = packageId;
        }
        public Guid PackageId { get; private set; }
    }
}
