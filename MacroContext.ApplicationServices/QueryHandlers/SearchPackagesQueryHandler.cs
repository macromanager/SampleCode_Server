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
using System.Linq.Expressions;
using MacroContext.Shared.ValueObjects;

namespace MacroContext.ApplicationServices.QueryHandlers
{
    public class SearchPackagesQueryHandler : IQueryHandler<SearchPackagesQuery, PagedResult<PackageDto>>
    {
        private IUnitOfWork _unitOfWork;
        private IMyMapper _mapper;

        public SearchPackagesQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PagedResult<PackageDto> Handle(SearchPackagesQuery query)
        {
            var paging = query.Paging;
            var searchStr = query.SearchString;
            PagedResult<Package> paged = null;
            if(query.UserId == Guid.Empty)
            {
                paged = _unitOfWork.Packages.Find(pkg => pkg.Name.Contains(searchStr), query.Paging);

                //packages = _unitOfWork.Packages.Find(pkg => pkg.Name.Contains(searchStr)).ToArray();
            }
            else
            {
                paged = _unitOfWork.Packages.Find(pkg => pkg.UserId == query.UserId && pkg.Name.Contains(searchStr), query.Paging);
                //packages = _unitOfWork.Packages.Find(pkg => pkg.UserId == query.UserId && pkg.Name.Contains(searchStr)).ToArray();
            }
            //var packageDtos = _mapper.Map<Package[], PackageDto[]>(packages.ToArray());
            //return packageDtos;

            var packageDtos = _mapper.Map<Package[], PackageDto[]>(paged.Result);
            var resultDto = new PagedResult<PackageDto>(paging, paged.PageCount, paged.ItemCount, packageDtos);
            return resultDto;

        }

    }
}
