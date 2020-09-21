using MacroContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Dto
{
    public class MacroDto
    {
        public MacroDto() { }
        public MacroDto(Guid id) { this.Id = id; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public MacroType MacroType { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
