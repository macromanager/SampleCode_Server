using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Shared.Utilities
{
    public interface IMyMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
