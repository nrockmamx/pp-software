using System;
using System.Runtime.InteropServices;
using static DemoApp.EVOSDK1Enums;
using static DemoApp.EVOSDK1Wrapper;
using static DemoApp.Source_Files.Helpers;

namespace DemoApp.Source_Files {
    class demoRibbonInfos : ICommand {
        string ICommand.Description => "Infos / Get ribbon infos";

        void ICommand.Execute(ref Data d) {
            IntPtr comHandle = IntPtr.Zero;
            IntPtr printerHandle = IntPtr.Zero;
            IntPtr pAnswer = IntPtr.Zero;
            string answer;
            int ec = (int)EPSDK_ReturnCode.EPSDK_INTERNAL_ERROR;

            if (DemoOpen(d.PrinterName, ref comHandle, ref printerHandle)) {

                ec = EPSDK_Infos_GetRibbonInfos(printerHandle, EPSDK_RibbonInfo.EPSDK_RI_Type, ref pAnswer);
                answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                Console.Write(" >> INFO_RIBBON_TYPE :                   "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { WriteAnswer(answer); } else { KO(); }

                pAnswer = IntPtr.Zero;
                ec = EPSDK_Infos_GetRibbonInfos(printerHandle, EPSDK_RibbonInfo.EPSDK_RI_Zone, ref pAnswer);
                answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                Console.Write(" >> INFO_RIBBON_ZONE :                   "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { WriteAnswer(answer); } else { KO(); }

                pAnswer = IntPtr.Zero;
                ec = EPSDK_Infos_GetRibbonInfos(printerHandle, EPSDK_RibbonInfo.EPSDK_RI_RemainingCapacity, ref pAnswer);
                answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                Console.Write(" >> INFO_RIBBON_REMAINING_CAPACITY :     "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { WriteAnswer(answer); } else { KO(); }

                pAnswer = IntPtr.Zero;
                ec = EPSDK_Infos_GetRibbonInfos(printerHandle, EPSDK_RibbonInfo.EPSDK_RI_TotalCapacity, ref pAnswer);
                answer = Marshal.PtrToStringAnsi(pAnswer).ToString();
                Console.Write(" >> INFO_RIBBON_TOTAL_CAPACITY :         "); if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) { WriteAnswer(answer); } else { KO(); }
                
            }

            DemoClose(comHandle, printerHandle);
        }
    }
}
