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
    public class GetAllPackagesQueryHandler : IQueryHandler<GetAllPackagesQuery, PagedResult<PackageDto>>
    {
        private IUnitOfWork _unitOfWork;
        private IMyMapper _mapper;

        public GetAllPackagesQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PagedResult<PackageDto> Handle(GetAllPackagesQuery query)
        {
            var paging = query.Paging;
            var paged = _unitOfWork.Packages.GetAll(query.Paging);
            var packageDtos = _mapper.Map<Package[], PackageDto[]>(paged.Result);
            var resultDto = new PagedResult<PackageDto>(paging, paged.PageCount, paged.ItemCount, packageDtos);
            return resultDto;

        }
    }
}
