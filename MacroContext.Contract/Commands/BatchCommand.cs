using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Commands
{
    public class BatchCommand : CommandBase
    {
        public BatchCommand(ICommand[] commands)
        {
            Commands = commands;
        }
        public ICommand[] Commands { get; set; }
    }
}
