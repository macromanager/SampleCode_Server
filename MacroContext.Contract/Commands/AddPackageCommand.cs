using MacroContext.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class AddPackageCommand: CommandBase
    {

        public AddPackageCommand(PackageDto package, ICommand[] macroCommands, ICommand[] referenceProfileCommands)
        {
            this.Package = package;
            this.MacrosCommands = macroCommands;
            this.ReferenceProfileCommands = referenceProfileCommands;

        }

        public PackageDto Package { get; set; }
        public ICommand[] MacrosCommands { get; set; }
        public ICommand[] ReferenceProfileCommands { get; set; }



    }
}
