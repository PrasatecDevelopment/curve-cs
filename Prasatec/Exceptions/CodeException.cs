using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec
{
    public class CodeException : Exception
    {
        public CodeException(string Message)
            : base("A code exception has occured: " + Message)
        {

        }
    }
}
