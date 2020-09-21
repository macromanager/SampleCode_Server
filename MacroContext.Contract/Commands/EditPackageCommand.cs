using MacroContext.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class EditPackageCommand: CommandBase
    {

        public EditPackageCommand(PackageDto package, ICommand[] macroCommands, ICommand[] referenceProfileCommands)
        {
            this.Package = package;
            this.MacroCommands = macroCommands;
            this.ReferenceProfileCommands = referenceProfileCommands;
        }

        public PackageDto Package { get; set; }
        public ICommand[] MacroCommands { get; set; }
        public ICommand[] ReferenceProfileCommands { get; set; }


    }
}
