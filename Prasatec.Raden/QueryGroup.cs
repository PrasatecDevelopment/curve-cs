using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal class QueryGroup : IQueryGroup
    {
        internal QueryGroup() { }
        internal string s_Identifier;

        public string Identifier { get { return this.s_Identifier; } }
    }
}
