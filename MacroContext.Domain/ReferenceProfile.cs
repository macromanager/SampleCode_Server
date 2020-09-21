using MacroContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Domain
{
    public class ReferenceProfile : IEntity<Guid>
    {
        internal ReferenceProfile() { }
        public ReferenceProfile(Guid id, Guid packageId, Guid referenceId)
        {
            this.Id = id;
            this.PackageId = packageId;
            this.ReferenceId = referenceId;
        }
        public Guid Id { get; private set; }
        public virtual Guid PackageId { get; private set; }
        public string Name { get; private set; }
        public Guid ReferenceId { get; private set; }
        public ReferenceVersion ReferenceVersion {get; private set;}
        public byte[] RowVersion { get; private set; }
       

        public void EditInfo(string name, ReferenceVersion version, byte[] rowVersion)
        {
            this.Name = name;
            this.ReferenceVersion = version;
            this.RowVersion = rowVersion;
            
        }
    }
}
