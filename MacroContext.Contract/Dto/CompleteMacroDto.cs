using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Dto
{
    public class CompleteMacroDto
    {
        public CompleteMacroDto() { }
        public CompleteMacroDto(MacroDto macro, MacroProfileDto macroProfile)
        {
            this.Macro = macro;
            this.MacroProfile = macroProfile;
        }

        public MacroDto Macro { get; set; }
        public MacroProfileDto MacroProfile { get; set; }
    }
}
