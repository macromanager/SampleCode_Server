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
    public class GetPackageQueryHandler : IQueryHandler<GetPackageQuery, PackageDto>
    {
        private IUnitOfWork _unitOfWork;
        private IMyMapper _mapper;

        public GetPackageQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PackageDto Handle(GetPackageQuery query)
        {
            var package = _unitOfWork.Packages.Get(query.PackageId);
            var packageDto = _mapper.Map<Package, PackageDto>(package);
            return packageDto;

        }
    }
}
