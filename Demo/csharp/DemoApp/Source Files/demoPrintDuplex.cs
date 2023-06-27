using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Source_Files {
    class demoPrintDuplex : ICommand {
        public string Description => "Print demo / Duplex YMCKOK";

        public void Execute(ref Data d) {
            demoPrint.Print(d, true);
        }
    }
}
