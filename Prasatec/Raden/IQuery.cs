using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface IQuery
    {
        KeyValuePair<string, object>[] Parameters { get; }
        string[] Tables { get; }
        DatabaseColumn[] Columns { get; }
        DatabaseCondition[] Conditions { get; }
        string[] GroupBy { get; }
        DatabaseSort[] SortBy { get; }
        int RecordLimit { get; }
        int StartingIndex { get; }
    }
}
