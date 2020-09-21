using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract
{
    [ServiceContract]
    public interface IClientConnectionService
    {
        [OperationContract]
        string Connect();
    }
}
