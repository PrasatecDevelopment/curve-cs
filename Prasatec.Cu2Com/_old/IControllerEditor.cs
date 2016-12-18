using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience
{
    public interface IControllerEditor<M> : IController
        where M : IModel
    {
        IConnection Connection { get; }
        Boolean Successful { get; }
        IWindowEditor Window { get; }
        M Model { get; }

        Boolean ShowCreator(IWin32Window owner);
        Boolean ShowEditor(IModel Model, IWin32Window owner);
        Boolean ShowViewer(IModel Model, IWin32Window owner);
    }
}
