using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience
{
    public interface IWindowEditor : IWindow
    {
        event EventHandler<GenericEventArgs<bool>> Saved;
        IElementDynamicTableLayout Fields { get; }
        Boolean CanChangeMode { get; set; }
        DynamicEditorModes Mode { get; set; }
        new IWindowEditor Base { get; }
        new void Show(IWin32Window owner);
        String EditTitle { get; set; }
        String ViewTitle { get; set; }
    }
}
