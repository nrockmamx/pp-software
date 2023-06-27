using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DemoApp.EVOSDK1Enums;
using static DemoApp.EVOSDK1Wrapper;
using static DemoApp.Source_Files.Helpers;

namespace DemoApp.Source_Files {
    class demoPrintTestCard : ICommand {

        IntPtr comHandle = IntPtr.Zero;
        IntPtr printerHandle = IntPtr.Zero;
        IntPtr pError = IntPtr.Zero;
        string error;

        string ICommand.Description => "Support tools / Print a test card";

        void ICommand.Execute(ref Data d) {
            int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;

            if (DemoOpen(d.PrinterName, ref comHandle, ref printerHandle)) {

                Console.WriteLine("=== PRINT A TEST CARD ===");
                Console.WriteLine(" >> Print in progress");

                ec = EPSDK_SupportTools_PrintTestCard(printerHandle, ref pError);
                Console.Write(" -> ");
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    OK();
                } else {
                    error = Marshal.PtrToStringAnsi(pError).ToString();
                    KO();
                    WriteColored(ec + "-" + error, ConsoleColor.Red);
                }

                DemoClose(comHandle, printerHandle);
            }
        }
    }
}
