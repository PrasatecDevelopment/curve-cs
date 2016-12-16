using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal class QueryConditionByValue : QueryCondition, IQueryConditionByValue
    {
        internal QueryConditionByValue() { }
        internal Object o_Value;
        internal String s_Wildcard;
        public Object Value { get { return this.o_Value; } }
        public String Wildcard { get { return this.s_Wildcard; } }
    }
    internal class QueryConditionActionableByValue : QueryConditionByValue, IQueryConditionActionable
    {
        internal QueryConditionActionableByValue() { }
        internal QueryColumnActions e_Action;
        public QueryColumnActions Action { get { return this.e_Action; } }
    }
}
