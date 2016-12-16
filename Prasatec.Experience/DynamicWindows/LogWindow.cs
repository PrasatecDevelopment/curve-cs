using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience.DynamicWindows
{
    public partial class LogWindow : BaseWindows.BasePaddedWindow, IWindowLog
    {
        public LogWindow()
        {
            InitializeComponent();
        }
    }
}
