using MacroContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Domain
{
    public class Macro: IEntity<Guid>
    {
        internal Macro() { }
        public Macro(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public byte[] RowVersion { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public MacroType MacroType { get; private set; }
        //public Guid[] AuthorIds { get; private set; }

        public void EditInfo(string name, string description, string code, MacroType macroType, byte[] rowVersion)
        {
            this.Name = name;
            this.Description = description;
            this.Code = code;
            this.MacroType = macroType;
            this.RowVersion = rowVersion;
        }

        
    
    }
}
