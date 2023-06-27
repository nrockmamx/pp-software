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
    class demoMagEncoding : ICommand {

        IntPtr comHandle = IntPtr.Zero;
        IntPtr printerHandle = IntPtr.Zero;
        char c;
        
        string ICommand.Description => "Mag encoding demo";

        void ICommand.Execute(ref Data d) {

            if (DemoOpen(d.PrinterName, ref comHandle, ref printerHandle)) {

                Console.WriteLine("=== MAG ENCODING API ===");

                Console.WriteLine(" ?? Please make sure your printer have a magnetic module then");
                Console.WriteLine(" ?? insert a card with magnetic track.");
                Console.Write(" ?? Start demo (y/n) ? ");
                c = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.WriteLine();

                if ('Y' == c || 'y' == c) {
                    IntPtr pAnswer = IntPtr.Zero;
                    string answer;
                    int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;
                    string[] dataToEncode = { "DATA", "1234", "5678" };

                    ec = EPSDK_MagEncoding_SetIsoNorm(printerHandle, 1, EPSDK_IsoNorm.EPSDK_IN_Iso1);
                    Console.Write(" >> Set isoNorm to {0} on track {1} = {1}.    ", EPSDK_IsoNorm.EPSDK_IN_Iso1, 1); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                    ec = EPSDK_MagEncoding_SetCoercivity(printerHandle, EPSDK_Coercivity.EPSDK_CO_Auto);
                    Console.Write(" >> Set coercivity to '{0}'.              ", EPSDK_Coercivity.EPSDK_CO_Auto); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                    for (int i = 0; i < dataToEncode.Length; i++) {
                        ec = EPSDK_MagEncoding_SetTrackData(printerHandle, 1 + i, dataToEncode[i]);
                        Console.Write(" >> Set data = '{0}' on track = {1}.       ", dataToEncode[i], 1 + i); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }
                    }

                    ec = EPSDK_MagEncoding_WriteTrackData(printerHandle);
                    Console.Write(" >> Write data on track.                  "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                    for(int i = 0; i < dataToEncode.Length; i++) {
                        ec = EPSDK_MagEncoding_ReadTrackData(printerHandle, 1 + i, ref pAnswer);
                        answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                        Console.Write(" >> Read '{0}' on track = {1}.             ", answer, 1 + i); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }
                    }

                    ec = EPSDK_SupportTools_MoveCard(printerHandle, EPSDK_CardDestination.EPSDK_CD_EjectCard);
                    Console.Write(" >> Ejecting card.                   "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                }
                else {
                    Console.WriteLine(" >> Skipped");
                }

                DemoClose(comHandle, printerHandle);
            }
        }
    }
}
