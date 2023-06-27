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
    class demoSendCommand : ICommand {

        IntPtr comHandle = IntPtr.Zero;
        IntPtr printerHandle = IntPtr.Zero;

        string ICommand.Description => "Support tools / Send a custom command";

        void ICommand.Execute(ref Data d) {
            int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;

            if (DemoOpen(d.PrinterName, ref comHandle, ref printerHandle)) {
                IntPtr pAnswer = IntPtr.Zero;
                string answer = string.Empty;

                Console.WriteLine("=== SEND A CUSTOM COMMAND ===");

                Console.WriteLine(" ?? Command to send to the printer: ");
                string cmd = Console.ReadLine();

                ec = EPSDK_SupportTools_SendCommand(printerHandle, cmd, ref pAnswer);
                Console.Write(" >> Sending command '{0}' to the printer.  ", cmd);
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    OK();
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                    Console.Write(" -- reply = "); WriteAnswer(answer);
                } else {
                    string ko = ec + " - " + answer;
                    KO(ko);
                    KO();
                }

                DemoClose(comHandle, printerHandle);
            }
        }
    }
}
