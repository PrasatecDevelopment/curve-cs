using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience
{
    public abstract class ApplicationBase<C>
        where C : IWindowMain
    {
        public ExecutiveBase Executive { get; private set; }
    }
}
