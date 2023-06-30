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
    class demoSupportTools : ICommand {

        IntPtr comHandle = IntPtr.Zero;
        IntPtr printerHandle = IntPtr.Zero;
        IntPtr pAnswer = IntPtr.Zero;
        string answer;

        string ICommand.Description => "Support tools demo";

        void ICommand.Execute(ref Data d) {
            int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;

            if(DemoOpen(d.PrinterName, ref comHandle, ref printerHandle)) {

                Console.WriteLine("=== SUPPORT TOOLS & PRINTER SETTINGS ===");
                Console.WriteLine(" >> Moving a card in the printer:");

                //demo of several printer settings and card movement
                ec = EPSDK_PrinterSettings_SetCardInsertion(printerHandle, EPSDK_InputTray.EPSDK_IT_Feeder);
                Console.Write(" -- Set card insertion to INPUT_FEEDER.  "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(); }

                //insert card in print position 
                ec = EPSDK_SupportTools_MoveCard(printerHandle, EPSDK_CardDestination.EPSDK_CD_PrintPos, 30000);
                Console.Write(" -- Move card to PRINT_POSITION.         "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(); }

                //move card to smart position, just for fun
                ec = EPSDK_SupportTools_MoveCard(printerHandle, EPSDK_CardDestination.EPSDK_CD_SmartPos, 30000);
                Console.Write(" -- Move card to SMART_POSITION.         "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(); }
                //
                //here the developper can use smart constructor SDK to perform ATR
                //operations, encode data on card etc...
                //

                //set card ejection to hopper
                ec = EPSDK_PrinterSettings_SetCardEjection(printerHandle, EPSDK_OutputTray.EPSDK_OT_Hopper);
                Console.Write(" -- Set card ejection to OUTPUT_HOPPER.  "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(); }

                //Eject card
                ec = EPSDK_SupportTools_MoveCard(printerHandle, EPSDK_CardDestination.EPSDK_CD_EjectCard, 30000);
                Console.Write(" -- Move card to EJECT_CARD.             "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(); }

                //Card preloading is only active when using monochrome ribbon.
                //Can be set but the card will only be inserted after performing
                //a monochrome printing.
                ec = EPSDK_PrinterSettings_SetCardPreloading(printerHandle, true);
                Console.Write(" -- Set preloading to TRUE.              "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(); }
                Console.WriteLine();

                //Card retaining options and settings are only available using KC model
                //Today demo we are not using a KC model for practical reason, but be
                //assured that these methods has been implemented and tested.

                //Send a command to the printer with EPSDK_SupportTools_SendCommand().
                //We are sending a 'Rfv' command to get the firmware version.
                pAnswer = IntPtr.Zero;
                ec = EPSDK_SupportTools_SendCommand(printerHandle, "Rfv", ref pAnswer);

                Console.Write(" >> Send a 'Rfv' command to the printer. "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    OK();
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                    Console.Write(" -- reply = "); WriteAnswer(answer);
                } else { KO(); }
                
                Console.WriteLine();

                //Read binary status of printer using GetDeviceBinaryStatus method
                //
                //The binary status is base64 encoded. It must be decoded to binary before use.
                //The EvoSdk1Base_DecodeBinary() method can be use to proceed.
                //
                //Full description of the binary data format can be found in the Premium SDK
                //Reference guide and Firmware documentation.
                pAnswer = IntPtr.Zero;
                ec = EPSDK_SupportTools_GetDeviceBinaryStatus(printerHandle, ref pAnswer);

                Console.Write(" >> Read binary status.                  "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    OK();
                    answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                    Console.Write(" -- reply = "); WriteAnswer(answer);
                } else { KO(); }

                

                DemoClose(comHandle, printerHandle);
            }
        }
    }
}
