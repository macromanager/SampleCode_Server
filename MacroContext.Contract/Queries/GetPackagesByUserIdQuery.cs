using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Dto;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Contract.Queries
{
    public class GetPackagesByUserIdQuery : PagingQueryBase, IQuery<PagedResult<PackageDto>>
    {
        public GetPackagesByUserIdQuery(Guid userId, PagingInformation paging)
        {
            this.UserId = userId;
            this.Paging = paging;
        }
        public Guid UserId { get; set; }

    }
}
