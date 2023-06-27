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
    class demoCleaningCycle : ICommand {

        IntPtr comHandle = IntPtr.Zero;
        IntPtr printerHandle = IntPtr.Zero;


        string ICommand.Description => "Support tools / Run cleaning cycle";

        void ICommand.Execute(ref Data d) {
            int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;

            if (DemoOpen(d.PrinterName, ref comHandle, ref printerHandle)) {
                char c;

                Console.WriteLine("=== CLEANING CYCLE ===");
                Console.WriteLine(" ?? To run cleaning cycle you need to insert a cleaning card.");
                Console.Write(" ?? Start cleaning (y/n) ? ");
                c = Console.ReadKey().KeyChar;

                if ('Y' == c || 'y' == c) {
                    IntPtr pError = IntPtr.Zero;
                    string error = string.Empty;

                    Console.WriteLine(" >> Cleaning in progress");

                    ec = EPSDK_SupportTools_RunCleaningCycle(printerHandle, ref pError);
                    if (IntPtr.Zero != pError) { error = Marshal.PtrToStringAnsi(pError).ToString(); }
                    Console.Write(" -> ");
                    if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                        OK();
                    } else {
                        string ko = ec + " - " + error;
                        KO(ko);
                    }

                } else {
                    Console.WriteLine(" >> Skipped");
                }

                DemoClose(comHandle, printerHandle);
            }


        }
    }
}
