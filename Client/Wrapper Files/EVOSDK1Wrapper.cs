using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class EVOSDK1Wrapper
    {
        
        #region  EvoSdk1Lib1 layer related
        
        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EvoSdk1Lib1_GetVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern string EvoSdk1Lib1_GetVersion();

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EvoSdk1Lib1_GetVersionW", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern string EvoSdk1Lib1_GetVersionW();

        #endregion

        #region GLOBAL API

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_GAPI_CreateCommunicationSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr EPSDK_GAPI_CreateCommunicationSettings(EVOSDK1Enums.EPSDK_ConnectionType connectionType, string param1, string param2);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_GAPI_OpenPrinter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr EPSDK_GAPI_OpenPrinter(string printerName, IntPtr comSettingHandle);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_GAPI_ClosePrinter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_GAPI_ClosePrinter(IntPtr printerHandle);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_GAPI_DestroyCommunicationSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_GAPI_DestroyCommunicationSettings(IntPtr comSettingHandle);

        #endregion

        #region SUPPORT TOOLS API

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_SupportTools_SendCommand", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_SupportTools_SendCommand(IntPtr printerHandle, string command, ref IntPtr answer, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_SupportTools_ResetCommunication", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_SupportTools_ResetCommunication(IntPtr printerHandle, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_SupportTools_GetDeviceBinaryStatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_SupportTools_GetDeviceBinaryStatus(IntPtr printerHandle, ref IntPtr answer, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_SupportTools_PrintTestCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_SupportTools_PrintTestCard(IntPtr printerHandle, ref IntPtr errorString, int timeout = 60000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_SupportTools_RunCleaningCycle", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_SupportTools_RunCleaningCycle(IntPtr printerHandle, ref IntPtr errorString, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_SupportTools_MoveCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_SupportTools_MoveCard(IntPtr printerHandle, EVOSDK1Enums.EPSDK_CardDestination cardPosition, int timeout = 30000);

        #endregion

        #region PRINTER SETTINGS

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_PrinterSettings_SetCardInsertion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_PrinterSettings_SetCardInsertion(IntPtr printerHandle, EVOSDK1Enums.EPSDK_InputTray value, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_PrinterSettings_SetCardEjection", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_PrinterSettings_SetCardEjection(IntPtr printerHandle, EVOSDK1Enums.EPSDK_OutputTray value, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_PrinterSettings_SetCardEjectionOnError", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_PrinterSettings_SetCardEjectionOnError(IntPtr printerHandle, EVOSDK1Enums.EPSDK_RejectBox value, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_PrinterSettings_SetCardPreloading", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_PrinterSettings_SetCardPreloading(IntPtr printerHandle, bool preloadCard, int timeout = 30000);


        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_PrinterSettings_SetCardRetainingLength", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_PrinterSettings_SetCardRetainingLength(IntPtr printerHandle, int lengthValue, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_PrinterSettings_SetCardRetainingDelayBeforeAction", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_PrinterSettings_SetCardRetainingDelayBeforeAction(IntPtr printerHandle, int lengthValue, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_PrinterSettings_SetCardRetainingAction", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_PrinterSettings_SetCardRetainingAction(IntPtr printerHandle, EVOSDK1Enums.EPSDK_BezelAction value, int timeout = 30000);

        #endregion

        #region MAG ECODING

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_MagEncoding_ReadTrackData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_MagEncoding_ReadTrackData(IntPtr printerHandle, int trackNumber, ref IntPtr outReadValue, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_MagEncoding_SetIsoNorm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_MagEncoding_SetIsoNorm(IntPtr printerHandle, int trackNumber, EVOSDK1Enums.EPSDK_IsoNorm iso, int timeout = 5000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_MagEncoding_SetTrackData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_MagEncoding_SetTrackData(IntPtr printerHandle, int trackNumber, string dataToEncode, int timeout = 5000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_MagEncoding_WriteTrackData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_MagEncoding_WriteTrackData(IntPtr printerHandle, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_MagEncoding_SetCoercivity", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_MagEncoding_SetCoercivity(IntPtr printerHandle, EVOSDK1Enums.EPSDK_Coercivity coercivity, int timeout = 5000);
        #endregion

        #region EPSDK - PRINTER & RIBBON INFOS API

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Infos_GetPrinterInfos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Infos_GetPrinterInfos(IntPtr printerHandle, EVOSDK1Enums.EPSDK_PrinterInfo printerInfo, ref IntPtr answer, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Infos_GetRibbonInfos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Infos_GetRibbonInfos(IntPtr printerHandle, EVOSDK1Enums.EPSDK_RibbonInfo ribbonInfo, ref IntPtr answer, int timeout = 30000);

        #endregion

        #region EPSDK - SUPERVISION API

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Supervision_EnumEvolisSupervisedPrinters", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Supervision_EnumEvolisSupervisedPrinters(IntPtr comHandle, int level, string modelName, ref IntPtr reply, int timeout = 30000);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Supervision_GetDeviceState", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Supervision_GetDeviceState(IntPtr printerHandle, ref IntPtr majorStatus, ref IntPtr minorStatus);

        #endregion

        #region PRINT API

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Print_Begin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Print_Begin(IntPtr printerHandle, ref IntPtr sessionId, string customSessionId = null);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Print_GetJobID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Print_GetJobID(IntPtr printerHandle, ref IntPtr sessionId);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Print_Set", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Print_Set(IntPtr printerHandle, string sessionId, string data);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Print_SetBitmap", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Print_SetBitmap(IntPtr printerHandle, EVOSDK1Enums.EPSDK_FaceType face, EVOSDK1Enums.EPSDK_PanelType panel, string sessionId, string data);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Print_Exec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Print_Exec(IntPtr printerHandle, string sessionId);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Print_End", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Print_End(IntPtr printerHandle, string sessionId);


        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Print_GetEvent", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Print_GetEvent(IntPtr printerHandle, ref IntPtr minorStatus, ref IntPtr actionList);

        [DllImport("libs\\EvoSdk1Lib1.dll", EntryPoint = "EPSDK_Print_SetEvent", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EPSDK_Print_SetEvent(IntPtr printerHandle, string minorStatus, string action);

        #endregion
    }
}
