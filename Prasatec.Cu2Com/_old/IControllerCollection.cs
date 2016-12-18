using Prasatec.Raden;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience
{
    public interface IControllerCollection<C, M> : IController
        where C : ICollection<M>
        where M : IModel
    {
        IConnection Connection { get; }
        C Collection { get; }
        IWindowCollection Window { get; }

        void Show();
        void Show(IWin32Window owner);
    }
}
