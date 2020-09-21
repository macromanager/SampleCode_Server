﻿using MacroContext.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.ApplicationServices.CommandHandlers
{
    public interface ICommandProcessor
    {
        void SubmitBatch(params ICommand[] commads);

    }
}
