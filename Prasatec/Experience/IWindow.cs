using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience
{
    public interface IWindow
    {
        IWindow Base { get; }
        IDesign Design { get; }

        Boolean Visible { get; set; }

        void Show();
        void Show(IWin32Window owner);
        void Close();
    }
}
