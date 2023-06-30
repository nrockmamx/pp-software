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
    class demoSupervision : ICommand {

        IntPtr comHandle = IntPtr.Zero;
        IntPtr printerHandle = IntPtr.Zero;

        string ICommand.Description => "Supervision demo";

        void ICommand.Execute(ref Data d) {

            if (DemoOpen(d.PrinterName, ref comHandle, ref printerHandle)) {
                IntPtr pAnswer = IntPtr.Zero;
                string answer;
                int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;

                Console.WriteLine("=== SUPERVISION API ===");
                Console.WriteLine(" >> Enumerate devices with major and minor states :");

                ec = EPSDK_Supervision_EnumEvolisSupervisedPrinters(comHandle, 2, d.ModelName, ref pAnswer);
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec) {
                    Console.WriteLine(" --Error: EPSDK_Supervision_EnumEvolisSupervisedPrinters() returned with code '{0}'.", ec);
                } else {
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                    Console.Write(" -- "); WriteAnswer(answer);
                    Console.WriteLine();
                }

                pAnswer = IntPtr.Zero;
                Console.WriteLine(" >> Enumerate devices without any state :");
                ec = EPSDK_Supervision_EnumEvolisSupervisedPrinters(comHandle, 0, d.ModelName, ref pAnswer);
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec) {
                    Console.WriteLine(" -- Error: EPSDK_Supervision_EnumEvolisSupervisedPrinters() returned with code '{0}'.", ec);
                } else {
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                    Console.Write(" -- "); WriteAnswer(answer);
                    Console.WriteLine();
                    List<string> printerList = answer.Split(';').ToList();
                    Console.WriteLine(" >> Getting states with EPSDK_Supervision_GetDeviceState() : ");

                    printerList.ForEach(printer => {
                        IntPtr ph = EPSDK_GAPI_OpenPrinter(printer, comHandle);

                        IntPtr pMajStatus = IntPtr.Zero;
                        IntPtr pMinStatus = IntPtr.Zero;

                    Console.Write(" --"); WriteColored(printer, ConsoleColor.Magenta);
                        if (null == ph) {
                            Console.WriteLine("    Error: Error getting the printer handle.");
                        } else {
                            ec = EPSDK_Supervision_GetDeviceState(ph, ref pMajStatus, ref pMinStatus);
                            string majStatus = Marshal.PtrToStringAnsi(pMajStatus).ToString();
                            string minStatus = Marshal.PtrToStringAnsi(pMinStatus).ToString();

                            Console.Write("    Device     : "); WriteAnswer(printer);
                            Console.Write("    majorStatus: "); WriteAnswer(majStatus);
                            Console.Write("    minorStatus: "); WriteAnswer(minStatus);
                        }
                    });
                }
                DemoClose(comHandle, printerHandle);

            }
        }
    }
}
