using System;
using System.Runtime.InteropServices;
using static DemoApp.EVOSDK1Enums;
using static DemoApp.EVOSDK1Wrapper;
using static DemoApp.Source_Files.Helpers;

namespace DemoApp.Source_Files {

    class demoCommunicationCheck : ICommand {

        string ICommand.Description  => "Communication check demo";
        IntPtr printerHandle = IntPtr.Zero;
        IntPtr comHandle = IntPtr.Zero;
        IntPtr pAnswer = IntPtr.Zero;
        string answer = string.Empty;

        int errorCode = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;
        

        public void Execute(ref Data d) {
            Console.WriteLine("=== COMMUNICATION CHECK  ===\n");

            Console.WriteLine(" >> PIPE:");
            comHandle = EPSDK_GAPI_CreateCommunicationSettings(EPSDK_ConnectionType.EPSDK_CT_Pipe, ".", "ESPFServer00");

            if(null != comHandle) {
                printerHandle = EPSDK_GAPI_OpenPrinter(d.PrinterName, comHandle);

                if( null != printerHandle) {
                    errorCode = EPSDK_SupportTools_ResetCommunication(printerHandle);
                    Console.Write(" -- Reset communication.      Status = ");
                    if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == errorCode) { OK(); } else { KO(); }

                    errorCode = EPSDK_SupportTools_SendCommand(printerHandle, "Rok", ref pAnswer);
                    if (IntPtr.Zero != pAnswer) { answer = Marshal.PtrToStringAnsi(pAnswer).ToString(); }

                    EPSDK_GAPI_ClosePrinter(printerHandle);
                }
                EPSDK_GAPI_DestroyCommunicationSettings(comHandle);
            }
            Console.Write(" -- PIPE connection.      Status = ");
            if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == errorCode && "OK" == answer) { OK(); } else { KO(); }

            Console.WriteLine();

            Console.WriteLine(" >> TCP:");
            errorCode = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;
            comHandle = EPSDK_GAPI_CreateCommunicationSettings(EPSDK_ConnectionType.EPSDK_CT_IP, "127.0.0.1", "18000");

            if (null != comHandle) {
                printerHandle = EPSDK_GAPI_OpenPrinter(d.PrinterName, comHandle);
                answer = string.Empty;

                if (null != printerHandle) {
                    errorCode = EPSDK_SupportTools_ResetCommunication(printerHandle);
                    Console.Write(" -- Reset communication.      Status = ");
                    if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == errorCode) { OK(); } else { KO(); }

                    errorCode = EPSDK_SupportTools_SendCommand(printerHandle, "Rok", ref pAnswer);
                    if (IntPtr.Zero != pAnswer) { answer = Marshal.PtrToStringAnsi(pAnswer).ToString(); }
                    
                    EPSDK_GAPI_ClosePrinter(printerHandle);
                }
                EPSDK_GAPI_DestroyCommunicationSettings(comHandle);
            }
            Console.Write(" -- TCP connection       Status = ");
            if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == errorCode && "OK" == answer) { OK(); } else { KO(); }
        }
    }
}
