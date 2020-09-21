using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Domain
{
    public class User : IEntity<Guid>
    {
        internal User() { }
        public User(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public byte[] RowVersion { get; private set; }

        public void EditInfo(string name, byte[] rowVersion)
        {
            this.Name = name;
            this.RowVersion = rowVersion;
        }
    }
}
