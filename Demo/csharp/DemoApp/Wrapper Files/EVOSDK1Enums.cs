using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    public struct Data {

        public Data(string modelName) {
            this.ModelName = modelName;
            this.PrinterName = "";
            Quit = false;
        }

        public string ModelName { get; set; }
        public string PrinterName { get; set; }
        public bool Quit { get; set; }


    }

    public class EVOSDK1Enums
    {
        public enum EPSDK_ConnectionType {
            EPSDK_CT_Unknown,
            EPSDK_CT_Pipe,
            EPSDK_CT_IP,
        };

        public enum EPSDK_FaceType {
            EPSDK_FT_Front,
            EPSDK_FT_Back,
        };

        public enum EPSDK_PanelType {
            EPSDK_PT_Color,
            EPSDK_PT_Resin,
            EPSDK_PT_Varnish,
        };

        public enum EPSDK_IsoNorm {
            EPSDK_IN_Iso1 = 1,
            EPSDK_IN_Iso2 = 2,
            EPSDK_IN_Iso3 = 3,
        };

        public enum EPSDK_Coercivity {
            EPSDK_CO_Hico = 'h',
            EPSDK_CO_Loco = 'l',
            EPSDK_CO_Auto = 'a',
        };

        public enum EPSDK_CardDestination {
            EPSDK_CD_PrintPos,
            EPSDK_CD_BackPos,
            EPSDK_CD_SmartPos,
            EPSDK_CD_ContactLessPos,
            EPSDK_CD_EjectCard,
            EPSDK_CD_RejectCard,
            EPSDK_CD_InsertEjectCard,
        };

        public enum EPSDK_InputTray {
            EPSDK_IT_Feeder,
            EPSDK_IT_Auto,
            EPSDK_IT_Manual,
            EPSDK_IT_Bezel,
        };
        public enum EPSDK_OutputTray {
            EPSDK_OT_Hopper,
            EPSDK_OT_Rear,
            EPSDK_OT_Bezel,
            EPSDK_OT_Manual,
            EPSDK_OT_LowerSlot,
        };
        public enum EPSDK_RejectBox {
            EPSDK_RB_Hopper,
            EPSDK_RB_Default,
            EPSDK_RB_LowerSlot,
        };
        public enum EPSDK_BezelAction {
            EPSDK_BA_Rejected,
            EPSDK_BA_Inserted,
            EPSDK_BA_Nothing,
        };
        public enum EPSDK_RibbonInfo {
            EPSDK_RI_Type,
            EPSDK_RI_Zone,
            EPSDK_RI_RemainingCapacity,
            EPSDK_RI_TotalCapacity,
            EPSDK_RI_BatchNumber,
            EPSDK_RI_ProductCode,
            EPSDK_RI_ManufacturedDate,
            EPSDK_RI_Description,
        };
        public enum EPSDK_PrinterInfo {
            EPSDK_PI_Fw,
            EPSDK_PI_Name,
            EPSDK_PI_Zone,
            EPSDK_PI_Model,
            EPSDK_PI_Duplex,
            EPSDK_PI_SmartOffset,
            EPSDK_PI_RetainingLength,
            EPSDK_PI_RetainingDelay,
            EPSDK_PI_RetainingAction,
            EPSDK_PI_PrintedPanelsCount,
            EPSDK_PI_InsertedCardsCount,
            EPSDK_PI_OptionEthernet,
            EPSDK_PI_OptionWifi,
            EPSDK_PI_OptionMag,
            EPSDK_PI_OptionSmart,
            EPSDK_PI_OptionContactLess,
            EPSDK_PI_OptionFlip,
            EPSDK_PI_OptionLaminator,
        };

        public enum EPSDK_ReturnCode {
            // No error
            EPSDK_NO_ERROR = 0,

            // 1 -> 99: Errors concerning the SDK usage.
            EPSDK_INTERNAL_ERROR = 1,
            EPSDK_INVALID_HANDLE = 2,
            EPSDK_INVALID_PARAMETER = 3,
            EPSDK_TIMEOUT = 4,
            EPSDK_ERR_NO_COMMUNICATION_WITH_ESPF_SERVICE = 5,
            EPSDK_ERR_NO_COMMUNICATION_WITH_PRINTER = 6,
            EPSDK_SEE_ERROR_STRING = 7,
            EPSDK_INVALID_CMD = 8,
            EPSDK_INVALID_CMD_PARAM = 9,

            // 100 -> 199: Mag encoding errors.
            EPSDK_ME_NOT_AVAILABLE = 100,
            EPSDK_ME_BLANK_TRACK = 101,
            EPSDK_ME_BAD_DATA_FORMAT = 102,
            EPSDK_ME_WRITE_ERROR = 103,
            EPSDK_ME_FEEDER_EMPTY = 104,

            // 1000 -> 1199: SUPERVISION service errors.
            EPSDK_SV_SET_ACTION_ERROR = 1001,
            EPSDK_SV_DEVICE_NOT_FOUND = 1100,
            EPSDK_SV_NO_PENDING_EVENT = 1103,
            EPSDK_SV_INVALID_ACTION = 1104,
            EPSDK_SV_INVALID_EVENT = 1105,

            // 1200 -> 1399: Errors concerning the communication between
            //               the library and the Evolis Premium SDK.
            EPSDK_CD_DEVICE_NOT_FOUND = 1302,
            EPSDK_CD_CANT_GET_STATUS = 1303,

            // 1600 -> 1799: PRINT service errors.
            EPSDK_PR_PRINT_ERROR = 1600,
            EPSDK_PR_SESSION_ALREADY_RESERVED = 1700,
            EPSDK_PR_INVALID_PRINTING_SESSION = 1701,
            EPSDK_PR_ACTION_ALREADY_IN_PROGRESS = 1702,
            EPSDK_PR_SETTING_OR_BITMAP_MISSING = 1703,
            EPSDK_PR_PROCESSING_ERROR = 1704,
            EPSDK_PR_JOB_CANCELED = 1705,
            EPSDK_PR_NO_PRINTING_SESSION = 1706,
        };
    }
}
