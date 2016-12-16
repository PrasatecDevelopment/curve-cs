using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal sealed class Query : IQuery
    {
        internal KeyValuePair<string, object>[] o_Parameters;
        internal string[] o_Tables;
        internal DatabaseColumn[] o_Columns;
        internal DatabaseCondition[] o_Conditions;
        internal string[] o_GroupBy;
        internal DatabaseSort[] o_SortBy;
        internal Int32 i_RecordLimit, i_StartingRecord;
        public KeyValuePair<string, object>[] Parameters { get { return o_Parameters; } }
        public string[] Tables { get { return o_Tables; } }
        public DatabaseColumn[] Columns { get { return o_Columns; } }
        public DatabaseCondition[] Conditions { get { return o_Conditions; } }
        public string[] GroupBy { get { return o_GroupBy; } }
        public DatabaseSort[] SortBy { get { return o_SortBy; } }
        public Int32 RecordLimit { get { return i_RecordLimit; } }
        public Int32 StartingIndex { get { return i_StartingRecord; } }
    }
}
