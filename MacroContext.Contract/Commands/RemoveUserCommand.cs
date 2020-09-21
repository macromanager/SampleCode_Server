using MacroContext.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class RemoveUserCommand: CommandBase
    {

        public RemoveUserCommand(UserDto user)
        {
            this.User = user;
        }

        public UserDto User { get; set; }

  
    }
}
