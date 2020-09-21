using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Shared.ValueObjects
{
    public class PackageLibraryMetadata
    {
        public PackageLibraryMetadata(int totalPackages, string packageNameWithMostDownloads)
        {
            this.TotalPackages = totalPackages;
            this.PackageNameWithMostDownloads = packageNameWithMostDownloads;
        }

        public int TotalPackages { get; set; }
        public string PackageNameWithMostDownloads { get; set; }
    }
}
