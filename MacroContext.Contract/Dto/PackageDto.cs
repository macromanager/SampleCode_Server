using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Dto
{
    public class PackageDto
    {
        public PackageDto() { }
        public PackageDto(Guid id) { this.Id = id; }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Downloads { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
