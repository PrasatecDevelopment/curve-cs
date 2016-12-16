using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public struct DatabaseCondition
    {
        public string Table1 { get; private set; }
        public string Column1 { get; private set; }
        public QueryLogicalOperators LogicalOperator { get; private set; }
        public QueryComparisonOperators ComparisonOperator { get; private set; }
        public Int32 Depth { get; private set; }

        public Boolean CanPerformAction { get; private set; }
        public QueryColumnActions Action { get; private set; }

        public string Table2 { get; private set; }
        public string Column2 { get; private set; }

        public object Value { get; private set; }
        public string Wildcard { get; private set; }

        public Boolean CompareColumns { get; private set; }
        public DatabaseCondition(string Table1, string Column1, QueryLogicalOperators LogicalOperator, QueryComparisonOperators ComparisonOperator, Int32 Depth)
        {
            this.Table1 = Table1;
            this.Column1 = Column1;
            this.LogicalOperator = LogicalOperator;
            this.ComparisonOperator = ComparisonOperator;
            this.Depth = Depth;
            this.CanPerformAction = false;
            this.Action = QueryColumnActions.None;
            this.Table2 = null;
            this.Column2 = null;
            this.Value = null;
            this.Wildcard = "*";
            this.CompareColumns = false;
        }
        public void SetColumn(string Table2, string Column2)
        {
            this.Table2 = Table2;
            this.Column2 = Column2;
            this.CompareColumns = true;
        }
        public void SetValue(object Value, string Wildcard)
        {
            this.Value = Value;
            this.Wildcard = Wildcard;
            this.CompareColumns = false;
        }
        public void SetAction(QueryColumnActions Action)
        {
            this.CanPerformAction = true;
            this.Action = Action;
        }
    }
}
