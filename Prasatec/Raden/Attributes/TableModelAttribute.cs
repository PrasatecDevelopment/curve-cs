using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public sealed class TableModelAttribute : Attribute
    {
        public String Name { get; private set; }
        public String IdColumn { get; private set; }
        public TableModelAttribute(string Table, string IdColumn)
        {
            this.Name = Table;
            this.IdColumn = IdColumn;
        }
    }
}
