using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Dto;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Contract.Queries
{
    public class SearchPackagesQuery : PagingQueryBase, IQuery<PagedResult<PackageDto>>
    {
        public SearchPackagesQuery() { }
        public SearchPackagesQuery(string searchString, Guid userId = default(Guid), PagingInformation paging = null)
        {
            SearchString = searchString;
            UserId = userId;
            this.Paging = paging;
        }
        public string SearchString { get; set; }
        public Guid UserId { get; set; }

    }
}
