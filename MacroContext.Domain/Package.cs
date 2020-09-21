using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Domain
{
    public class Package: IEntity<Guid>
    {
        internal Package() { }
        public Package(Guid id)
        {
            Id = id;
            MacroProfiles = new List<MacroProfile>();
            ReferenceProfiles = new List<ReferenceProfile>();
        }

        public Guid UserId { get; private set; }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public byte[] RowVersion { get; private set; }
        public string Description { get; private set; }
        public int Downloads { get; private set; }
        public virtual ICollection<MacroProfile> MacroProfiles { get; private set; }
        public virtual ICollection<ReferenceProfile> ReferenceProfiles { get; private set; }
        
        public void EditInfo(Guid userId, string name, string description, byte[] rowVersion)
        {
            this.UserId = userId;
            this.Name = name;
            this.Description = description;
            this.RowVersion = rowVersion;
        }

        public void Downloaded()
        {
            this.Downloads++;
        }

    }
}
