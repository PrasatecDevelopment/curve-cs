using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public struct DatabaseColumn
    {
        public string Table { get; private set; }
        public string Name { get; private set; }
        public string Alias { get; private set; }
        public QueryColumnActions Action { get; private set; }
        public DatabaseColumn(string Table, string Name, string Alias, QueryColumnActions Action)
        {
            this.Table = Table;
            this.Name = Name;
            this.Alias = Alias;
            this.Action = Action;
        }
    }
}
