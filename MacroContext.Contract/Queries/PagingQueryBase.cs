using MacroContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Queries
{
    public abstract class PagingQueryBase
    {
        public PagingInformation Paging { get; set; }
    }
}
