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
    public partial class OperationWindow : BaseWindows.BasePaddedWindow, IWindowOperation
    {
        public OperationWindow()
        {
            InitializeComponent();
        }
    }
}
