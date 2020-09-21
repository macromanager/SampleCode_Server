using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MacroContext.Infrastructure.FatalErrorHandlers
{
    public static class FatalExceptionHandler
    {
        //public static ILog Logger { get; set; }
        public static string ErrorMessage { get; set; }

        static FatalExceptionHandler()
        {
            //Logger = Bootstrapper.Logger;
            ErrorMessage = "An unknown error occured. This application session has ended";
        }

        public static void Handle(Exception e)
        {
            //Debugger.Break();
            //Logger.Fatal("a fatal error occured", e);
            MessageBox.Show(ErrorMessage, "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();

        }
    }
}
