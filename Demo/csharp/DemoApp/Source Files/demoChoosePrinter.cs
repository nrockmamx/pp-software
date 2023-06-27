using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static DemoApp.EVOSDK1Enums;
using static DemoApp.EVOSDK1Wrapper;
using static DemoApp.Source_Files.Helpers;

namespace DemoApp.Source_Files {
    class demoChoosePrinter : ICommand {
        string ICommand.Description => "Change the target printer";

        void ICommand.Execute(ref Data d) {
            IntPtr handle = IntPtr.Zero;
            Console.WriteLine("=== TARGET PRINTER ===");
            handle = EVOSDK1Wrapper.EPSDK_GAPI_CreateCommunicationSettings(EVOSDK1Enums.EPSDK_ConnectionType.EPSDK_CT_Pipe, ".", "ESPFServer00");

            if (null != handle) {
                IntPtr reply = IntPtr.Zero;
                int ec;
                int index = 0;

                ec = EVOSDK1Wrapper.EPSDK_Supervision_EnumEvolisSupervisedPrinters(handle, 0, d.ModelName, ref reply);
                // If more than one printer available then "reply" will contain
                // a string like "<PRINTER-0>;<PRINTER-1>;<PRINTER-N>" with
                // <PRINTER-X> replaced with the name of the printer.

                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {

                    string printers = Marshal.PtrToStringAnsi(reply).ToString();
                    List<string> printerList = printers.Split(';').ToList();

                    if (0 == printerList.Count) {
                        Console.WriteLine(" >> No printer available for this model.");
                        d.Quit = true;
                    } else if (1 == printerList.Count) {
                        Console.WriteLine(" >> Only one printer available.");
                        Console.WriteLine(" >> Selecting printer " + printerList.First().ToString());
                    } else {
                        int c = 0;
                        printerList.ForEach(x => {
                            Console.WriteLine(c + " - " + x.ToString());
                            c++;
                        });
                        Console.Write("Please choose a printer number and press Enter : ");
                        index = Convert.ToInt32(Console.ReadLine());
                    }

                    d.PrinterName = printerList.ElementAt(index);
                }

                EVOSDK1Wrapper.EPSDK_GAPI_DestroyCommunicationSettings(handle);
            }
        }
    }
}
