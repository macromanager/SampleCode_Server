using MacroContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Dto
{
    public class MacroProfileDto
    {
        public MacroProfileDto() { }
        public MacroProfileDto(Guid id, Guid packageId, Guid macroId)
        {
            this.Id = id;
            this.PackageId = packageId;
            this.MacroId = macroId;
        }

        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public Guid MacroId { get; set; }
        public int MacroPosition { get; set; }
        public ComponentType ComponentType { get; set; }
        public string ComponentName { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
