using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience.Elements
{
    public class PaginationButton : Button, IButtonElement
    {
        public PaginationButton(IWindow Controller)
            : base(Controller)
        {
            this.AutoSize = false;
            this.MinimumSize = new System.Drawing.Size(Controller.Design.PaginationButtonWidth, Controller.Design.PaginationButtonHeight);
            this.Size = this.MinimumSize;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Controller.Design.BackgroundColor;
            this.ForeColor = Controller.Design.ForegroundColor;
        }
    }
}
