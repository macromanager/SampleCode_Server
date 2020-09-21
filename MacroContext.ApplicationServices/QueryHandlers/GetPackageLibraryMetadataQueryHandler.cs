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
using MacroContext.Shared.ValueObjects;

namespace MacroContext.ApplicationServices.QueryHandlers
{
    public class GetPackageLibraryMetadataQueryHandler : IQueryHandler<GetPackageLibraryMetadata,PackageLibraryMetadata>
    {
        private IUnitOfWork _unitOfWork;
        private IMyMapper _mapper;

        public GetPackageLibraryMetadataQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PackageLibraryMetadata Handle(GetPackageLibraryMetadata query)
        {
            var result = _unitOfWork.Packages.GetPackageLibraryMetadata(query.UserId);
            return result;
        }
    }
}
