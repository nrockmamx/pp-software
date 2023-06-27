using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DemoApp.EVOSDK1Enums;
using static DemoApp.EVOSDK1Wrapper;

namespace DemoApp.Source_Files {
    public static class Helpers {

        #region Colored terminal output methods
        public static void OK() {
            WriteColored("SUCCESS", ConsoleColor.Green);
        }
        public static void KO(object ec = null) {
            if (null != ec) { ec = "WITH ERRROR CODE " + ec; }
            else { ec = string.Empty; }

            WriteColored("FAILED " + ec.ToString(), ConsoleColor.Red);
        }
        public static void WriteAnswer(string s) {
            WriteColored(s, ConsoleColor.DarkCyan);
        }
        public static void WriteColored(string s, ConsoleColor cs) {
            Console.ForegroundColor = cs;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        #endregion

        public static bool DemoOpen(string printerName, ref IntPtr sh, ref IntPtr ph) {
            if(null != sh) {
                sh = EPSDK_GAPI_CreateCommunicationSettings(EPSDK_ConnectionType.EPSDK_CT_Pipe, ".", "ESPFServer00");
                if(null != sh && null != ph) {
                    ph = EPSDK_GAPI_OpenPrinter(printerName, sh);
                    if (null == ph) {
                        EPSDK_GAPI_DestroyCommunicationSettings(sh);
                        sh = IntPtr.Zero;
                        return false;
                    }
                    else return true;
                }
            }
            return false;
        }

        public static void DemoClose(IntPtr sh, IntPtr ph) {
            if (null != ph) {
                EPSDK_GAPI_ClosePrinter(ph);
            }

            if(null != sh) {
                EPSDK_GAPI_DestroyCommunicationSettings(sh);
            }
        }
    }
}
