using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience.BaseWindows
{
    public partial class BasePaddedWindow : BaseWindow, IWindow
    {
        public BasePaddedWindow()
        {
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Padding = new Padding(8);
        }

        /*void IWindow.ShowDialog()
        {
            Base.ShowDialog();
        }

        void IWindow.ShowDialog(IWin32Window owner)
        {
            Base.ShowDialog(owner);
        }*/
    }
}
