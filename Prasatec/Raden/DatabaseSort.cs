using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public struct DatabaseSort
    {
        public string Table { get; private set; }
        public string Column { get; private set; }
        public QuerySortDirections Direction { get; private set; }

        public DatabaseSort(String Table, String Name, QuerySortDirections Direction)
        {
            this.Table = Table;
            this.Column = Name;
            this.Direction = Direction;
        }
    }
}
