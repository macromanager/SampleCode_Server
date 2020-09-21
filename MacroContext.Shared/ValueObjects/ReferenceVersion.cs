using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Shared.ValueObjects
{
    public class ReferenceVersion
    {
        public ReferenceVersion() { }
        public ReferenceVersion(int major, int minor)
        {
            this.Major = major;
            this.Minor = minor;
        }
        public int Major { get; set; }
        public int Minor { get; set; }
    }
}
