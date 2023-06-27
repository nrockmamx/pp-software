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

    class demoPrinterInfos : ICommand {
        string ICommand.Description => "Infos / Get printer infos";

        public void Execute(ref Data d) {
            IntPtr comHandle = IntPtr.Zero;
            IntPtr printerHandle = IntPtr.Zero;
            IntPtr pAnswer = IntPtr.Zero;
            string answer = string.Empty;
            string ko = string.Empty;
            int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;

            if (DemoOpen(d.PrinterName, ref comHandle, ref printerHandle)) {

                Console.WriteLine("=== PRINTER INFOS ===");

                ec = EPSDK_Infos_GetPrinterInfos(printerHandle, EPSDK_PrinterInfo.EPSDK_PI_Fw, ref pAnswer);
                if( IntPtr.Zero!= pAnswer) {
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                }
                Console.Write(" >> INFO_PRINTER_FW  :                   ");
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    WriteAnswer(answer);
                } else {
                    ko = ec + " - " + answer;
                    KO(ko);
                }

                pAnswer = IntPtr.Zero;
                answer = string.Empty;
                ec = EPSDK_Infos_GetPrinterInfos(printerHandle, EPSDK_PrinterInfo.EPSDK_PI_Name, ref pAnswer);
                if (IntPtr.Zero != pAnswer) {
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                }
                Console.Write(" >> INFO_PRINTER_NAME :                  ");
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    WriteAnswer(answer);
                } else {
                    ko = ec + " - " + answer;
                    KO(ko);
                }

                pAnswer = IntPtr.Zero;
                answer = string.Empty;
                ec = EPSDK_Infos_GetPrinterInfos(printerHandle, EPSDK_PrinterInfo.EPSDK_PI_Zone, ref pAnswer);
                if (IntPtr.Zero != pAnswer) {
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                }
                Console.Write(" >> INFO_PRINTER_ZONE :                  ");
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    WriteAnswer(answer);
                } else {
                    ko = ec + " - " + answer;
                    KO(ko);
                }

                pAnswer = IntPtr.Zero;
                answer = string.Empty;
                ec = EPSDK_Infos_GetPrinterInfos(printerHandle, EPSDK_PrinterInfo.EPSDK_PI_Model, ref pAnswer);
                if (IntPtr.Zero != pAnswer) {
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                }
                Console.Write(" >> INFO_PRINTER_MODEL :                 ");
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    WriteAnswer(answer);
                } else {
                    ko = ec + " - " + answer;
                    KO(ko);
                }

                pAnswer = IntPtr.Zero;
                answer = string.Empty;
                ec = EPSDK_Infos_GetPrinterInfos(printerHandle, EPSDK_PrinterInfo.EPSDK_PI_Duplex, ref pAnswer);
                if (IntPtr.Zero != pAnswer) {
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                }
                Console.Write(" >> INFO_PRINTER_DUPLEX :                ");
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    WriteAnswer(answer);
                } else {
                    ko = ec + " - " + answer;
                    KO(ko);
                }
            }

            DemoClose(comHandle, printerHandle);
        }
    }
}
