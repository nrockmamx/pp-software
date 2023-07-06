using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client
{
    public class PassportInfo
    {
        public bool Used = true;
        public string Type { get; set; }
        public string Angle { get; set; }
        public string Mrtds { get; set; }
        public string DocumentNo { get; set; }
        public string PersonalNo { get; set; }
        public string Familyname { get; set; }
        public string Givenname { get; set; }
        public string Nationality { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public string Dateofexpiry { get; set; }
        public string IssueState { get; set; }
        public string NativeName { get; set; }
    }
    public static class PassportScanner
    {
        public static PassportInfo passportInfo { get; set; }
        static System.Timers.Timer timer;
        static int interval = 200; // 0.2 seconds
        static int totalTime = 60 * 1000; // 60 seconds
        static int elapsedTime = 0; // Elapsed time in ms
        static DeviceWrapper.LIBWFXEVENTCB m_CBEvent;
        public static bool Stop { get; set; } = true;
        
        public static async void Start()
        {
            

            ENUM_LIBWFX_ERRCODE enErrCode;
            bool DoScan = false;
            int timer = 0, sum = 0;
            IntPtr pScanImageList, pOCRResultList, pExceptionRet, pEventRet;
            string Command = "{\"device-name\":\"A62\",\"source\":\"Camera\",\"recognize-type\":\"passport\"}";

            //get command from bat file "AutoCaptureDemo-CSharp.bat"
            String[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Length > 1)
                Command = arguments[1];

            enErrCode = DeviceWrapper.LibWFX_InitEx(ENUM_LIBWFX_INIT_MODE.LIBWFX_INIT_MODE_NORMAL);

            if (enErrCode == ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_NO_OCR)
                System.Console.WriteLine(@"Status:[No Recognize Tool]");
            else if (enErrCode == ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_NO_AVI_OCR)
                System.Console.WriteLine(@"Status:[No AVI Recognize Tool]");
            else if (enErrCode == ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_NO_DOC_OCR)
                System.Console.WriteLine(@"Status:[No DOC Recognize Tool]");
            else if (enErrCode == ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_PATH_TOO_LONG)
            {
                MessageBox.Show(@"Status:[Path Is Too Long (max limit: 130 bits)]");
                MessageBox.Show(@"Status:[LibWFX_InitEx Fail]");
            }
            else if (enErrCode != ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_SUCCESS)
            {
                MessageBox.Show(@"Status:[LibWFX_InitEx Fail [" + ((int)enErrCode).ToString() + "]] "); //get fail message
                return;
            }

            enErrCode = DeviceWrapper.LibWFX_SetProperty(Command, m_CBEvent, IntPtr.Zero);
            if (enErrCode != ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_SUCCESS)
            {
                IntPtr pstr;
                DeviceWrapper.LibWFX_GetLastErrorCode(enErrCode, out pstr);
                string szErrorMsg = Marshal.PtrToStringUni(pstr);
               MessageBox.Show(@"Status:[LibWFX_SetProperty Fail [" + ((int)enErrCode).ToString() + "]] " + szErrorMsg.ToString()); //get fail message
            }

            Stop = false;
            while (!Stop)
            {
                timer = 0;
                sum = 0;
                while (timer < 3 && !Stop)
                {
                    await Task.Delay(100);
                    sum++;
                    enErrCode = DeviceWrapper.LibWFX_PaperReady();
                    if (enErrCode == ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_SUCCESS)
                        timer++;

                    if (sum == 4)
                    {
                        sum = 0;
                        timer = 0;
                        if (DoScan)
                            DoScan = false;
                        //System.Console.WriteLine(@"Please put the card");
                        await Task.Delay(1000);
                    }
                }

                if (DoScan)
                {
                    //System.Console.WriteLine(@"The card is continuously detected, please remove the card.");
                    await Task.Delay(1000);
                    continue;
                }

                enErrCode = DeviceWrapper.LibWFX_SynchronizeScan(Command, out pScanImageList, out pOCRResultList, out pExceptionRet, out pEventRet);

                string szExceptionRet = Marshal.PtrToStringUni(pExceptionRet);
                string szEventRet = Marshal.PtrToStringUni(pEventRet);

                if (enErrCode != ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_SUCCESS && enErrCode != ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_COMMAND_KEY_MISMATCH)
                {
                    IntPtr pstr;
                    DeviceWrapper.LibWFX_GetLastErrorCode(enErrCode, out pstr);
                    string szErrorMsg = Marshal.PtrToStringUni(pstr);
                    //System.Console.WriteLine(@"Status:[LibWFX_SynchronizeScan Fail [" + ((int)enErrCode).ToString() + "]] " + szErrorMsg.ToString()); //get fail message
                }
                else if (szEventRet.Length > 1) //event happen
                {
                    //System.Console.WriteLine(@"Status:[Device Ready!]");
                    //System.Console.WriteLine(szEventRet);  //get event message

                    if (szEventRet != "LIBWFX_EVENT_UVSECURITY_DETECTED[0]" && szEventRet != "LIBWFX_EVENT_UVSECURITY_DETECTED[1]")
                    {
                        //System.Console.WriteLine(@"Status:[Scan End]\n");
                        return;
                    }

                    //if (enErrCode == ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_COMMAND_KEY_MISMATCH)
                        System.Console.WriteLine(@"Status:[There are some mismatched key in command]");

                    string szScanImageList = Marshal.PtrToStringUni(pScanImageList);
                    string szOCRResultList = Marshal.PtrToStringUni(pOCRResultList);


                    string[] ScanImageWords = szScanImageList.Split(']');
                    string[] OCRResultWords = szOCRResultList.Split(']');

                    for (int idx = 0; idx < ScanImageWords.Length - 1; idx++)
                    {
                        System.Console.WriteLine(ScanImageWords[idx].Trim());  //get each image path
                        System.Console.WriteLine(OCRResultWords[idx].Trim());  //get each ocr result
                    }
                }
                else
                {
                    System.Console.WriteLine(@"Status:[Device Ready!]");

                    if (enErrCode == ENUM_LIBWFX_ERRCODE.LIBWFX_ERRCODE_COMMAND_KEY_MISMATCH)
                        System.Console.WriteLine(@"Status:[There are some mismatched key in command]");

                    if (szExceptionRet.Length > 1) //exception happen
                    {
                        //System.Console.WriteLine(@"Status:[Device Ready!]");
                        //System.Console.WriteLine(@szExceptionRet);  //get exception message
                    }

                    string szScanImageList = Marshal.PtrToStringUni(pScanImageList);
                    string szOCRResultList = Marshal.PtrToStringUni(pOCRResultList);


                    string[] ScanImageWords = szScanImageList.Split(']');
                    string[] OCRResultWords = szOCRResultList.Split(']');

                    //for (int idx = 0; idx < ScanImageWords.Length - 1; idx++)
                    //{
                    //    System.Console.WriteLine(ScanImageWords[idx].Trim());  //get each image path
                    //    System.Console.WriteLine(OCRResultWords[idx].Trim());  //get each ocr result
                    //}

                    passportInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<PassportInfo>(OCRResultWords[0].Trim());
                    
                    if(passportInfo!=null)
                    {
                        File.Copy(ScanImageWords[0],"Passportimage/temp.jpg", true);
                        passportInfo.Used = false;
                    }

                }
                //System.Console.WriteLine(@"Status:[Scan End]");
                DoScan = true;
                GC.Collect();

                await Task.Delay(1000);
            }
        }
    }
}
