using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract
{
    public class AmqpInfo
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public string ExchangeName { get; set; }
    }
}
