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
    public class CheckPackageUniqueNameQueryHandler : IQueryHandler<CheckPackageUniqueNameQuery, bool>
    {
        private IUnitOfWork _unitOfWork;
        private IMyMapper _mapper;

        public CheckPackageUniqueNameQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool Handle(CheckPackageUniqueNameQuery query)
        {
            var packages = _unitOfWork.Packages.FindOne((pkg) => pkg.Name == query.PackageName);
            var numberOfPkgsWithNameAllowed = query.IsNewPackage ? 0 : 1;
            var isUnique = false;
            if(packages.Count() == 0)
            {
                isUnique = true;
            }
            else
            {
                if (!query.IsNewPackage)
                {
                    var pkg = packages.Single();
                    if(query.PackageId == pkg.Id)
                    {
                        isUnique = true;
                    }
                }
            }
            return isUnique;
        }
    }
}
