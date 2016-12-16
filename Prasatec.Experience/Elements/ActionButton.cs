using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience.Elements
{
    public class ActionButton : Button, IButtonElement
    {
        public ActionButton(IWindow Controller)
            : base(Controller)
        {
            this.MinimumSize = new System.Drawing.Size(Controller.Design.ActionButtonWidth, Controller.Design.ActionButtonHeight);
            this.Size = this.MinimumSize;
        }
    }
}
