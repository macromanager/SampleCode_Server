using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract
{
    public class ConnectToServerRequest
    {
        public ConnectToServerRequest(string uriConnection)
        {
            UriConnection = uriConnection;
        }
        public string UriConnection { get; set; }
    }
}
