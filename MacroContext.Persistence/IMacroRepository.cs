using MacroContext.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Persistance
{
    public interface IMacroRepository: IRepository<Macro,Guid>
    {
    }
}
