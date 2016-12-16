using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal class QueryCondition : IQueryCondition
    {
        internal QueryCondition() { }
        internal int i_Depth;
        internal string s_Identifier;
        internal QueryLogicalOperators e_Logical;
        internal QueryComparisonOperators e_Operator;

        public int Depth { get { return this.i_Depth; } }
        public string Identifier { get { return this.s_Identifier; } }
        public QueryLogicalOperators Logical { get { return this.e_Logical; } }
        public QueryComparisonOperators Operator { get { return this.e_Operator; } }
    }
}
