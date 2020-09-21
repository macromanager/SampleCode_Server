using MacroContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Dto
{
    public class ReferenceProfileDto
    {
        public ReferenceProfileDto() { }
        public ReferenceProfileDto(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public string Name { get; set; }
        public Guid ReferenceId { get; set; }
        public ReferenceVersion ReferenceVersion { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
