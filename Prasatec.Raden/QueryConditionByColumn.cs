using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal class QueryConditionByColumn : QueryCondition, IQueryConditionByColumn
    {
        internal QueryConditionByColumn() { }
        internal string s_ValueColumn;
        public String ValueColumn { get { return this.s_ValueColumn; } }
    }
    internal class QueryConditionActionableByColumn : QueryConditionByColumn, IQueryConditionActionable
    {
        internal QueryConditionActionableByColumn() { }
        internal QueryColumnActions e_Action;
        public QueryColumnActions Action { get { return this.e_Action; } }
    }
}
