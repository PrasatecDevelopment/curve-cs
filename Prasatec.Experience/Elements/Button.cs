using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience.Elements
{
    public class Button : System.Windows.Forms.Button, IButtonElement
    {
        public IWindow Controller { get; private set; }
        public Button()
        {

        }
        public Button(IWindow Controller)
        {
            this.AutoSize = true;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Controller.Design.ButtonColor;
            this.FlatAppearance.BorderSize = 1;
            this.FlatAppearance.CheckedBackColor = Controller.Design.ToggleColor;
            this.FlatAppearance.MouseDownBackColor = Controller.Design.ActiveColor;
            this.FlatAppearance.MouseOverBackColor = Controller.Design.HoverColor;
            this.BackColor = Controller.Design.ButtonColor;
            this.ForeColor = Helpers.DesignHelper.GetAdjustedForeColor(this.BackColor);
            this.Padding = new System.Windows.Forms.Padding(Controller.Design.PaddingBase);
            this.MinimumSize = new System.Drawing.Size(Controller.Design.ButtonWidth, Controller.Design.ButtonHeight);
            this.Size = this.MinimumSize;
        }
    }
}
