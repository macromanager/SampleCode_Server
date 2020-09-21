using MacroContext.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class RemoveReferenceProfilesFromPackageCommand : CommandBase
    {
        public RemoveReferenceProfilesFromPackageCommand(ICollection<ReferenceProfileDto> referenceProfiles)
        {
            this.ReferenceProfiles = referenceProfiles;
        }
        public ICollection<ReferenceProfileDto> ReferenceProfiles { get; set; }

    }
}
