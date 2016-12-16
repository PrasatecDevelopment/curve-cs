using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal sealed class QuerySort : IQuerySort
    {
        internal QuerySort() { }
        internal String s_Identifier;
        internal QuerySortDirections e_Direction;

        public QuerySortDirections Direction { get { return e_Direction; } }
        public string Identifier { get { return s_Identifier; } }
    }
}
