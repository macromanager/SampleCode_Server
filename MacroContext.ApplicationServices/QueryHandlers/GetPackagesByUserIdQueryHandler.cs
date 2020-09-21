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
    public class GetPackagesByUserIdQueryHandler : IQueryHandler<GetPackagesByUserIdQuery, PagedResult<PackageDto>>
    {
        private IUnitOfWork _unitOfWork;
        private IMyMapper _mapper;

        public GetPackagesByUserIdQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PagedResult<PackageDto> Handle(GetPackagesByUserIdQuery query)
        {
            var paged = _unitOfWork.Packages.GetPackagesByUserId(query.UserId, query.Paging);
            var packageDtos = _mapper.Map<Package[], PackageDto[]>(paged.Result.ToArray());
            var result = new PagedResult<PackageDto>(paged.Paging, paged.PageCount, paged.ItemCount, packageDtos);
            return result;
        }
    }
}
