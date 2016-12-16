using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface IQueryCondition
    {
        String Identifier { get; }
        QueryLogicalOperators Logical { get; }
        QueryComparisonOperators Operator { get; }
        Int32 Depth { get; }
    }
}
