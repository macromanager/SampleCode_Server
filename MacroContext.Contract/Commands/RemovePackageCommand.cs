using MacroContext.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class RemovePackageCommand: CommandBase
    {

        public RemovePackageCommand(PackageDto package)
        {
            this.Package = package;
        }

        public PackageDto Package { get; set; }

  
    }
}
