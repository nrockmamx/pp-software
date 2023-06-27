using DemoApp.Source_Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DemoApp.EVOSDK1Enums;
using static DemoApp.EVOSDK1Wrapper;
using static DemoApp.Source_Files.Helpers;

namespace DemoApp
{
    public class Program
    {
        
        static IntPtr pHandle = IntPtr.Zero;

        static void ChoosePrinter(ref Data d) {
            IntPtr handle = IntPtr.Zero;
            Console.WriteLine("=== TARGET PRINTER ===");
            handle = EVOSDK1Wrapper.EPSDK_GAPI_CreateCommunicationSettings(EVOSDK1Enums.EPSDK_ConnectionType.EPSDK_CT_Pipe, ".", "ESPFServer00");

            if (null != handle) {
                IntPtr reply = IntPtr.Zero;
                int ec;
                int index = 0;

                ec = EVOSDK1Wrapper.EPSDK_Supervision_EnumEvolisSupervisedPrinters(handle, 0, d.ModelName, ref reply);
                // If more than one printer available then "reply" will contain
                // a string like "<PRINTER-0>;<PRINTER-1>;<PRINTER-N>" with
                // <PRINTER-X> replaced with the name of the printer.

                if ((int)EPSDK_ReturnCode.EPSDK_NO_ERROR == ec) {

                    string printers = Marshal.PtrToStringAnsi(reply).ToString();
                    List<string> printerList = printers.Split(';').ToList();

                    if (0 == printerList.Count) {
                        Console.WriteLine(" >> No printer available for this model.");
                        d.Quit = true;
                    } else if (1 == printerList.Count) {
                        Console.WriteLine(" >> Only one printer available.");
                        Console.WriteLine(" >> Selecting printer " + printerList.First().ToString());
                    } else {
                        int c = 0;
                        printerList.ForEach(x => {
                        Console.Write(c + " - "); WriteColored(x.ToString(), ConsoleColor.Magenta);
                            c++;
                        });
                        Console.Write("Please choose a printer number and press Enter : ");
                        index = Convert.ToInt32(Console.ReadLine());
                    }

                    d.PrinterName = printerList.ElementAt(index);
                }

                EVOSDK1Wrapper.EPSDK_GAPI_DestroyCommunicationSettings(handle);
            }

        }

        /// <summary>
        /// Main for demo EvoSdk1Lib1
        /// </summary>
        /// <param name="args">Array of command input arguments </param>
        /// <returns>Error code</returns>
        static int Main(string[] args)
        {
            Data data = new Data("Evolis Primacy");

            ChoosePrinter(ref data);

            var commands = new ICommand[]
                {
                    new demoChoosePrinter(),
                    new demoCommunicationCheck(),
                    new demoPrinterInfos(),
                    new demoRibbonInfos(),
                    new demoMagEncoding(),
                    new demoPrintSimplex(),
                    new demoPrintDuplex(),
                    new demoSupervision(),
                    new demoSupportTools(),
                    new demoPrintTestCard(),
                    new demoCleaningCycle(),
                    new demoSendCommand(),
                    new ExitCommand()
                };

            while (!data.Quit) {

                Console.WriteLine("\n\n=== MENU ===\n");
                Console.WriteLine(" >> Printer model: {0}", data.ModelName);
                Console.Write(" >> Printer name : ");
                WriteColored(data.PrinterName, ConsoleColor.Magenta);
                Console.WriteLine();

                for (int i = 0; i < commands.Length; i++) {
                    Console.WriteLine("{0} - {1}", i, commands[i].Description);
                }

                var userChoice = string.Empty;
                var commandIndex = -1;

                do {
                    Console.WriteLine("\n?? Please, what is your option ? ");
                    userChoice = Console.ReadLine();
                }
                while (!int.TryParse(userChoice, out commandIndex) || commandIndex > commands.Length);

                commands[commandIndex].Execute(ref data);
            }

            return 0;
            
        }      
    }
}
