using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Dto;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Contract.Queries
{
    public class GetAllUsersQuery : PagingQueryBase, IQuery<PagedResult<UserDto>>
    {
        public GetAllUsersQuery(PagingInformation paging)
        {
            this.Paging = paging;
        }
    }
}
