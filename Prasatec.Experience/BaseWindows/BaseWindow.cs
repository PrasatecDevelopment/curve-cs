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
    public partial class BaseWindow : Form, IWindow
    {
        private IDesign o_Design;
        public IWindow Base { get { return this; } }
        public IDesign Design
        {
            get { return this.o_Design; }
            set
            {
                if (value == null)
                {
                    // Use reflection to set properties back to normal
                }
                else
                {
                    this.o_Design = value;
                }
            }
        }
        public BaseWindow()
        {
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
        }
    }
}
