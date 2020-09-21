using MacroContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Domain
{
    public class MacroProfile : IEntity<Guid>
    {
        internal MacroProfile() { }
        public MacroProfile(Guid id, Guid packageId, Macro macro)
        {
            this.Id = id;
            this.PackageId = packageId;
            this.Macro = macro;
        }
        public Guid Id { get; private set; }
        public virtual Macro Macro { get; private set; }
        public virtual Guid PackageId { get; private set; }
        public int MacroPosition { get; private set; }
        public ComponentType ComponentType { get; set; }
        public string ComponentName { get; private set; }
        public byte[] RowVersion { get; private set; }
        
        public void UpdateProfile(int macroPosition, ComponentType componentType, string componentName, byte[] rowVersion)
        {
            this.MacroPosition = macroPosition;
            this.ComponentName = componentName;
            this.ComponentType = componentType;
            this.RowVersion = rowVersion;
        }

    }
}
