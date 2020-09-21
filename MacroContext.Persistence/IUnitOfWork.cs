using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MacroContext.Domain;

namespace MacroContext.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IMacroRepository Macros { get; }
        IMacroProfileRepository MacroProfiles { get; }
        IPackageRepository Packages { get; }
        IUserRepository Users { get; }
        IReferenceProfileRepository ReferenceProfiles { get; }
        int Complete(bool ignoreConcurrency = false);
    }
}
