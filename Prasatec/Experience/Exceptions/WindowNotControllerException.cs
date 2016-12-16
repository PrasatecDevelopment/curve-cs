using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience
{
    public class WindowNotControllerException : CodeException
    {
        public WindowNotControllerException(String SourceType, String ControllerType)
            : base(string.Format("{0} must implement {1}", SourceType, ControllerType))
        {

        }
    }
}
