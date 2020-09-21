using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MacroContext.Domain;
using MacroContext.Contract.Dto;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.Infrastructure.Abstractions.Mapper
{
    public class ReferenceProfileProfile : Profile
    {
        public ReferenceProfileProfile()
        {
            CreateMap<ReferenceProfile, ReferenceProfileDto>();
            CreateMap<ReferenceVersion, ReferenceVersion>();
        }
    }
}
