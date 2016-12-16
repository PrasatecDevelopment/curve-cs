using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec
{
    public class BuilderLockedException : Exception
    {
        public BuilderLockedException(string Name)
            : base(Name + " has been locked and cannot be changed.")
        {

        }
    }
}
