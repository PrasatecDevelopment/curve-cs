using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class AutoIncrementAlreadySetException : Exception
    {
        public AutoIncrementAlreadySetException()
            : base("Only one value can be set as auto increment and that will always be ID")
        {

        }
    }
}
