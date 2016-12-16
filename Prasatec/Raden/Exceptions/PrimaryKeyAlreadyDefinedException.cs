using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class PrimaryKeyAlreadyDefinedException : Exception
    {
        public PrimaryKeyAlreadyDefinedException()
            : base("Only one primary key can be defined an that will always be ID")
        {

        }
    }
}
