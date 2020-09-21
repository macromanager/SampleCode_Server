using MacroContext.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class AddReferenceProfilesToPackageCommand : CommandBase
    {

        public AddReferenceProfilesToPackageCommand(ICollection<ReferenceProfileDto> referencesProfiles)
        {
            this.ReferencesProfiles = referencesProfiles;
        }
        public ICollection<ReferenceProfileDto> ReferencesProfiles{ get; set; }

  
    }
}
