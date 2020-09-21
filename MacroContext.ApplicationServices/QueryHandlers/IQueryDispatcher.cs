using MacroContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.ApplicationServices.QueryHandlers
{
    public interface IQueryDispatcher
    {
        TResult Submit<TResult>(IQuery<TResult> query);
    }
}
