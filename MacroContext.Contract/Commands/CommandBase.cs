using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class CommandBase : ICommand
    {
        public Guid Id { get; set; }
    }
}
