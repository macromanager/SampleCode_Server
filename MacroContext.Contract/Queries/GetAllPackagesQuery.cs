using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Dto;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Contract.Queries
{
    public class GetAllPackagesQuery : PagingQueryBase, IQuery<PagedResult<PackageDto>>
    {
        public GetAllPackagesQuery(PagingInformation paging)
        {
            this.Paging = paging;
        }
    }
}
