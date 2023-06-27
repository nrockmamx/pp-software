using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Source_Files {
    class ExitCommand : ICommand {
        string ICommand.Description => "Quit.";

        void ICommand.Execute(ref Data data) {
            Console.WriteLine("Quit.");
            data.Quit = true;
            Environment.Exit(0);
        }
    }
}
