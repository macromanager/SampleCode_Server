using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Dto;

namespace MacroContext.Contract.Queries
{
    public class GetUserQuery : IQuery<UserDto>
    {
        public GetUserQuery() { }
        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; set; }
    }
}
