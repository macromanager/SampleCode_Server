using MacroContext.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class EditMacrosCommand : CommandBase
    {
        public EditMacrosCommand(ICollection<CompleteMacroDto> completeMacros)
        {
            this.CompleteMacros = completeMacros;
        }
        public ICollection<CompleteMacroDto> CompleteMacros { get; set; }
    }
}
