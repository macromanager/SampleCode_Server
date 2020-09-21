using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MacroContext.Domain;
using MacroContext.Contract.Dto;

namespace MacroContext.Infrastructure.Abstractions.Mapper
{
    public class MacroProfile: Profile
    {
        public MacroProfile()
        {
            CreateMap<Macro, MacroDto>();
        }
    }
}
