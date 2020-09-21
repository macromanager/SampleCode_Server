using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.FatalErrorHandlers
{
    public static class FatalExceptionObject
    {
        public static void Handle(object exceptionObject)
        {
            var errObj = exceptionObject as Exception;
            if (errObj == null)
            {
                errObj = new NotSupportedException(
                  "Unhandled exception doesn't derive from System.Exception: "
                   + exceptionObject.ToString()
                );
            }
            FatalExceptionHandler.Handle(errObj);
        }
    }
}
