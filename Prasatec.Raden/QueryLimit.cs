using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal class QueryLimit : IQueryLimit
    {
        internal QueryLimit() { }
        internal int i_RecordLimit, i_StartingIndex;
        public int RecordLimit { get { return i_RecordLimit; } }
        public int StartingIndex { get { return i_StartingIndex; } }
    }
}
