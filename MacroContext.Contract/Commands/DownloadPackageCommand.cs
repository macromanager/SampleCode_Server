using MacroContext.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class DownloadPackageCommand : CommandBase
    {

        public DownloadPackageCommand(Guid packageId)
        {
            this.PackageId = packageId;
        }

        public Guid PackageId { get; set; }

  
    }
}
