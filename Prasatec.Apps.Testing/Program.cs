using Prasatec.Cu2Com;
using Prasatec.Cu2Com.Data;
using Prasatec.Cu2Com.Experience;
using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Apps.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(true);
            Application.EnableVisualStyles();
            Console.Write("Connecting to MySql Server...");
            var c = new ConnectionBuilder<Raden.Engines.MysqlEngine>()
                .Server("nctslaws")
                .Database("cu2com")
                .Username("cu2com")
                .Password("cu2com")
                .Build();
            Console.WriteLine("Done");

            var controller = new UserController(c);
            controller.ShowCollection();
            while (true) { Application.DoEvents(); }
            /*return;
            var result = controller.Create()
                .Name("test1")
                .Mode(65535)
                .Execute();
            Console.WriteLine("{0} {1}", result.Successful, result.ErrorMessage);
            /*var mgr = new ManageLogsController(c);
            mgr.Show();
            while (mgr.Window.Visible) { Application.DoEvents(); }*/
        }
    }
}
