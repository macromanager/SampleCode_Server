using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Persistance;
using MacroContext.Domain;
using MacroContext.Contract.Queries;
using MacroContext.Contract.Dto;
using MacroContext.Shared.Utilities;

namespace MacroContext.ApplicationServices.QueryHandlers
{
    public class GetMacrosByPackageIdQueryHandler : IQueryHandler<GetMacrosByPackageIdQuery, CompleteMacroDto[]>
    {
        private IUnitOfWork _unitOfWork;
        private IMyMapper _mapper;

        public GetMacrosByPackageIdQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CompleteMacroDto[] Handle(GetMacrosByPackageIdQuery query)
        {
            var profiles = _unitOfWork.MacroProfiles.GetByPackageId(query.PackageId);
            var macros = GetCompleteMacros(profiles);
            return macros.ToArray();

        }

        public IEnumerable<CompleteMacroDto> GetCompleteMacros(IEnumerable<MacroProfile> profiles)
        {
            var completeMacros = new List<CompleteMacroDto>();
            foreach (var profile in profiles)
            {
                var macroDto = _mapper.Map<Macro, MacroDto>(profile.Macro);
                var profileDto = _mapper.Map<MacroProfile, MacroProfileDto>(profile);
                var completeMacroDto = new CompleteMacroDto(macroDto, profileDto);
                completeMacros.Add(completeMacroDto);
            }
            return completeMacros;
        }
    }
}
