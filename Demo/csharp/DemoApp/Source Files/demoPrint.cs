using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DemoApp.EVOSDK1Enums;
using static DemoApp.EVOSDK1Wrapper;
using static DemoApp.Source_Files.Helpers;

namespace DemoApp.Source_Files {

    public class demoPrint {

        public static void Print(Data d, bool doDuplex) {
            IntPtr printerHandle = IntPtr.Zero;
            IntPtr comHandle = IntPtr.Zero;
            IntPtr pAnswer = IntPtr.Zero;
            string answer = string.Empty;
            int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;

            if (DemoOpen(d.PrinterName, ref comHandle, ref printerHandle)) {

                Console.WriteLine("=== PRINT SIMPLEX API ===");

                string printingParams = (doDuplex)
                   /* Duplex YMCKOK */ ? "GRibbonType=RC_YMCKOK;Duplex=HORIZONTAL;GDuplexType=DUPLEX_CM;FOverlayManagement=FULLVARNISH" 
                   /* Simplex YMCKO */ : "GRibbonType=RC_YMCKO;FOverlayManagement=FULLVARNISH";
                string frontFileData = string.Empty;
                string backFileData = string.Empty;
                bool endFlag = false;

                ec = EPSDK_Print_Begin(printerHandle, ref pAnswer);
                Console.Write(" >> Begin print session.               "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                if ((int)EPSDK_ReturnCode.EPSDK_PR_SESSION_ALREADY_RESERVED == ec) {

                    pAnswer = IntPtr.Zero;
                    EPSDK_Print_GetJobID(printerHandle, ref pAnswer);
                    char c;
                    answer = Marshal.PtrToStringAnsi(pAnswer);

                    Console.WriteLine(" >> Session already reserved: {0}", answer);
                    Console.WriteLine(" ?? Do you want to end that session (y/n) ?");
                    c = Console.ReadKey().KeyChar;
                    if ('Y' == c || 'y' == c) {
                        ec = EPSDK_Print_End(printerHandle, answer);
                        Print(d, doDuplex);
                    }

                } else if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec) {
                    Console.WriteLine(" >> Aborting");
                } else {
                    // Backup the session id to be sure it's not overriden by
                    // the library.
                    answer = Marshal.PtrToStringAnsi(pAnswer);
                    string sessionID = answer;

                    ec = EPSDK_Print_Set(printerHandle, sessionID, printingParams);
                    Console.Write(" >> Set printing parameters.           "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }


                    // It's mandatory to encode the file to base 64 before
                    // sending it to the API.
                    Bitmap fBmp = new Bitmap("resources/frontBitMap.bmp");
                    MemoryStream frontMemStream = new MemoryStream();

                    fBmp.Save(frontMemStream, ImageFormat.Bmp);
                    byte[] fBuffer = frontMemStream.ToArray();
                    frontMemStream.Close();
                    frontFileData = Convert.ToBase64String(fBuffer);
                    
                    ec = EPSDK_Print_SetBitmap(printerHandle, EPSDK_FaceType.EPSDK_FT_Front, EPSDK_PanelType.EPSDK_PT_Color, sessionID, frontFileData);
                    Console.Write(" >> Setting EPSDK_FT_Front bitmap.     "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                    fBuffer = null;
                    frontFileData = string.Empty;

                    if(doDuplex) {
                        //Load front bmp data into base64 encoded string
                        Bitmap bBmp = new Bitmap("resources/backBitMap.bmp");
                        MemoryStream backMemStream = new MemoryStream();

                        bBmp.Save(backMemStream, ImageFormat.Bmp);
                        byte[] bBuffer = backMemStream.ToArray();
                        backMemStream.Close();
                        backFileData = Convert.ToBase64String(bBuffer);

                        ec = EPSDK_Print_SetBitmap(printerHandle, EPSDK_FaceType.EPSDK_FT_Back, EPSDK_PanelType.EPSDK_PT_Resin, sessionID, backFileData);
                        Console.Write(" >> Setting EPSDK_FT_Back bitmap.      "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                        bBuffer = null;
                        backFileData = string.Empty;
                    }

                    // The printing process must be launched from the current thread.
                    // The thread will be blocked until the printing was done.
                    //
                    // To receive events from the printer during the print process, we
                    // have to start a new thread that will be in charge of polling the
                    // printer for events.
                    Thread printThread = new Thread(() => {
                        printSessionManagement(printerHandle, ref endFlag);
                    });

                    printThread.Start();

                    Console.Write(" >> EPSDK_Print_Print() started.       "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                    ec = EPSDK_Print_Exec(printerHandle, sessionID);
                    Console.Write(" >> EPSDK_Print_Print() end.           "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                    endFlag = true;

                    ec = EPSDK_Print_End(printerHandle, sessionID);
                    Console.Write(" >> Ending print session.              "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                    printThread.Join();

                }

                DemoClose(comHandle, printerHandle);

            }
        }

        static void printSessionManagement(IntPtr printerHandle, ref bool endFlag) {
            IntPtr pMinorState = IntPtr.Zero;
            IntPtr pActionList = IntPtr.Zero;
            string minorState = string.Empty;
            string actionList = string.Empty;
            string actionEvent = string.Empty;
            int seconds = 0;
            int ec;

            while(!endFlag) {
                ec = EPSDK_Print_GetEvent(printerHandle, ref pMinorState, ref pActionList);
                Console.WriteLine(" >> EPSDK_Print_GetEvent() -> {0} - alive for {1} seconds", ec, seconds);
                if((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {
                    minorState = Marshal.PtrToStringAnsi(pMinorState).ToString();
                    actionList = Marshal.PtrToStringAnsi(pActionList).ToString();
                    Console.WriteLine(" -- minorState = {0}", minorState);
                    Console.WriteLine(" -- actionList = {0}", actionList);
                    if("NONE" != minorState) {
                        Console.WriteLine(" ?? Please type in your desired action: ");
                        actionEvent = Console.ReadLine();
                        ec = EPSDK_Print_SetEvent(printerHandle, minorState, actionEvent);
                        Console.Write(" >> EPSDK_Print_SetEvent() ->   "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }
                    }
                }

                Thread.Sleep(3000);
                seconds += 3;
            }
            
        }
    }
}
