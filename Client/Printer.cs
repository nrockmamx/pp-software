using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Client.EVOSDK1Enums;
using static Client.EVOSDK1Wrapper;

namespace Client
{
    public class Printer
    {
        

        public void ChoosePrinter(ref Data d, string frontFile, string backFile)
        {
            IntPtr handle = IntPtr.Zero;
            handle = EVOSDK1Wrapper.EPSDK_GAPI_CreateCommunicationSettings(EVOSDK1Enums.EPSDK_ConnectionType.EPSDK_CT_Pipe, ".", "ESPF2Server00");

            if (null != handle)
            {
                IntPtr reply = IntPtr.Zero;
                int ec;
                int index = 0;

                ec = EVOSDK1Wrapper.EPSDK_Supervision_EnumEvolisSupervisedPrinters(handle, 0, d.ModelName, ref reply);


                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec)
                {

                    string printers = Marshal.PtrToStringAnsi(reply).ToString();
                    List<string> printerList = printers.Split(';').ToList();

                    if (0 == printerList.Count)
                    {
                        MessageBox.Show(" >> No printer available for this model.", "Error Printer");
                        return;
                    }

                    d.PrinterName = printerList.ElementAt(index);
                }

                EVOSDK1Wrapper.EPSDK_GAPI_DestroyCommunicationSettings(handle);

                Print(d, frontFile, backFile);
            }

        }

        public void Print(Data d, string frontFile, string backFile)
        {
            IntPtr printerHandle = IntPtr.Zero;
            IntPtr comHandle = IntPtr.Zero;
            IntPtr pAnswer = IntPtr.Zero;
            string answer = string.Empty;
            int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;

            if (PrinterOpen(d.PrinterName, ref comHandle, ref printerHandle))
            {

                string printingParams = "GRibbonType=RC_YMCKO;Duplex=HORIZONTAL;GDuplexType=DUPLEX_CC;FOverlayManagement=FULLVARNISH;BOverlayManagement=FULLVARNISH;FBlackManagement=ALLBLACKPOINT;BBlackManagement=ALLBLACKPOINT;BOverlayContrast=VAL20;FOverlayContrast=VAL20;FColorContrast=VAL10;BColorContrast=VAL20;Gsmoothing=ADVSMOOTH";
                string frontFileData = string.Empty;
                string backFileData = string.Empty;
                bool endFlag = false;

                ec = EPSDK_Print_Begin(printerHandle, ref pAnswer);
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec)
                {
                    MessageBox.Show($"Printer Error {ec}");
                    pAnswer = IntPtr.Zero;
                    EPSDK_Print_GetJobID(printerHandle, ref pAnswer);
                    char c;
                    answer = Marshal.PtrToStringAnsi(pAnswer);

                    ec = EPSDK_Print_End(printerHandle, answer);
                    PrinterClose(comHandle, printerHandle);
                    return;
                }

                if ((int)EPSDK_ReturnCode.EPSDK_PR_SESSION_ALREADY_RESERVED == ec)
                {

                    pAnswer = IntPtr.Zero;
                    EPSDK_Print_GetJobID(printerHandle, ref pAnswer);
                    char c;
                    answer = Marshal.PtrToStringAnsi(pAnswer);

                    ec = EPSDK_Print_End(printerHandle, answer);



                    MessageBox.Show($"Printer not busy {ec}");
                    return;
                }
                else if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec)
                {
                    MessageBox.Show($"Aborting {ec}");
                }
                else
                {
                    answer = Marshal.PtrToStringAnsi(pAnswer);
                    string sessionID = answer;

                    ec = EPSDK_Print_Set(printerHandle, sessionID, printingParams);

                    if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec)
                    {
                        MessageBox.Show($"Printer error parameter {ec}");
                        return;
                    }

                    Bitmap fBmp = new Bitmap(frontFile);
                    MemoryStream frontMemStream = new MemoryStream();

                    fBmp.Save(frontMemStream, ImageFormat.Bmp);
                    byte[] fBuffer = frontMemStream.ToArray();
                    frontMemStream.Close();
                    frontFileData = Convert.ToBase64String(fBuffer);

                    ec = EPSDK_Print_SetBitmap(printerHandle, EPSDK_FaceType.EPSDK_FT_Front, EPSDK_PanelType.EPSDK_PT_Color, sessionID, frontFileData);
                    if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec)
                    {
                        MessageBox.Show($"Printer error front image {ec}");
                        return;
                    }


                    fBuffer = null;
                    frontFileData = string.Empty;

                    Bitmap bBmp = new Bitmap(backFile);
                    MemoryStream backMemStream = new MemoryStream();

                    bBmp.Save(backMemStream, ImageFormat.Bmp);
                    byte[] bBuffer = backMemStream.ToArray();
                    backMemStream.Close();
                    backFileData = Convert.ToBase64String(bBuffer);

                    ec = EPSDK_Print_SetBitmap(printerHandle, EPSDK_FaceType.EPSDK_FT_Back, EPSDK_PanelType.EPSDK_PT_Resin, sessionID, backFileData);
                    //Console.Write(" >> Setting EPSDK_FT_Back bitmap.      "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }

                    bBuffer = null;
                    backFileData = string.Empty;

                    //Thread printThread = new Thread(() => {
                    //    printSessionManagement(printerHandle, ref endFlag);
                    //});

                    //printThread.Start();

                    if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec)
                    {
                        MessageBox.Show($"Printer error  {ec}");
                        return;
                    }

                    ec = EPSDK_Print_Exec(printerHandle, sessionID);

                    if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec)
                    {
                        MessageBox.Show($"Printer error  {ec}");
                        return;
                    }


                    endFlag = true;

                    //ec = EPSDK_Print_End(printerHandle, sessionID);

                    if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR != ec)
                    {
                        MessageBox.Show($"Printer error  {ec}");
                        return;
                    }


                    //printThread.Join();

                }

                //PrinterClose(comHandle, printerHandle);

            }
        }

        static void printSessionManagement(IntPtr printerHandle, ref bool endFlag)
        {
            IntPtr pMinorState = IntPtr.Zero;
            IntPtr pActionList = IntPtr.Zero;
            string minorState = string.Empty;
            string actionList = string.Empty;
            string actionEvent = string.Empty;
            int seconds = 0;
            int ec;

            while (!endFlag)
            {
                ec = EPSDK_Print_GetEvent(printerHandle, ref pMinorState, ref pActionList);
                Console.WriteLine(" >> EPSDK_Print_GetEvent() -> {0} - alive for {1} seconds", ec, seconds);
                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec)
                {
                    minorState = Marshal.PtrToStringAnsi(pMinorState).ToString();
                    actionList = Marshal.PtrToStringAnsi(pActionList).ToString();
                    Console.WriteLine(" -- minorState = {0}", minorState);
                    Console.WriteLine(" -- actionList = {0}", actionList);
                    if ("NONE" != minorState)
                    {
                        Console.WriteLine(" ?? Please type in your desired action: ");
                        actionEvent = Console.ReadLine();
                        ec = EPSDK_Print_SetEvent(printerHandle, minorState, actionEvent);
                        //Console.Write(" >> EPSDK_Print_SetEvent() ->   "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { OK(); } else { KO(ec); }
                    }
                }

                Thread.Sleep(3000);
                seconds += 3;
            }

        }

        public bool PrinterOpen(string printerName, ref IntPtr sh, ref IntPtr ph)
        {
            if (null != sh)
            {
                sh = EPSDK_GAPI_CreateCommunicationSettings(EPSDK_ConnectionType.EPSDK_CT_Pipe, ".", "Espf2Server00");
                if (null != sh && null != ph)
                {
                    ph = EPSDK_GAPI_OpenPrinter(printerName, sh);
                    if (null == ph)
                    {
                        EPSDK_GAPI_DestroyCommunicationSettings(sh);
                        sh = IntPtr.Zero;
                        return false;
                    }
                    else return true;
                }
            }
            return false;
        }

        public void PrinterClose(IntPtr sh, IntPtr ph)
        {
            if (null != ph)
            {
                EPSDK_GAPI_ClosePrinter(ph);
            }

            if (null != sh)
            {
                EPSDK_GAPI_DestroyCommunicationSettings(sh);
            }
        }
    }
}