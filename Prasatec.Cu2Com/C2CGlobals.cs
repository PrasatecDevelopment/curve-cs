using Prasatec.Plink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com
{
    internal static class C2CGlobals
    {
        private static Definitions _def;
        internal static Definitions def
        {
            get
            {
                if (_def == null) { _def = Definitions.Retrieve(); }
                return _def;
            }
        }
    }
}
