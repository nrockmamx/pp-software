using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Source_Files {
    interface ICommand {
        string Description { get; }
        void Execute(ref Data data);
    }
}
