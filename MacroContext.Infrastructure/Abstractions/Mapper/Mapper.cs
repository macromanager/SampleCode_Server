using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MacroContext.Shared.Utilities;

namespace MacroContext.Infrastructure.Abstractions.Mapper
{
    public class MyMapper : IMyMapper
    {
        private IMapper _mapper;
        public MyMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public TDestination Map<TSource,TDestination>(TSource source)
        {
            var destination = _mapper.Map<TSource, TDestination>(source);
            return destination;
        }
    }
}
