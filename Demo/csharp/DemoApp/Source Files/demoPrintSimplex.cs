using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Source_Files {
    class demoPrintSimplex : ICommand {
        string ICommand.Description => "Print demo / Simplex YMCKO";

        void ICommand.Execute(ref Data d) {
            demoPrint.Print(d, false);
            
        }
    }
}
