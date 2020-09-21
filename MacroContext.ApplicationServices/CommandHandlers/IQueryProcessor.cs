using MacroContext.Contract.Commands;
using MacroContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.ApplicationServices.CommandHandlers
{
    public interface IQueryProcessor
    {
        TResult SubmitBatch<TResult>(IQuery<TResult> query);

    }
}
