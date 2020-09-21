using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Contract.Dto;

namespace MacroContext.Contract.Queries
{
    public class CheckPackageUniqueNameQuery : IQuery<bool>
    {
        public CheckPackageUniqueNameQuery(string packageName, Guid packageId, bool isNewPackage)
        {
            PackageName = packageName;
            PackageId = packageId;
            IsNewPackage = isNewPackage;
        }

        public string PackageName { get; set; }
        public bool IsNewPackage { get; set; }
        public Guid PackageId { get; set; }


    }
}
