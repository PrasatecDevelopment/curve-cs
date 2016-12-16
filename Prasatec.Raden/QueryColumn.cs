using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal class QueryColumn : IQueryColumn
    {
        internal QueryColumn() { }
        internal string s_Identifier, s_Alias;
        internal QueryColumnActions e_Action;

        public QueryColumnActions Action { get { return this.e_Action; } }
        public string Alias { get { return this.s_Alias; } }
        public string Identifier { get { return this.s_Identifier; } }
    }
}
