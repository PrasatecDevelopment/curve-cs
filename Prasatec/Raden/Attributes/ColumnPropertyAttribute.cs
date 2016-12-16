using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ColumnPropertyAttribute : Attribute
    {
        public String Name { get; private set; }
        public ColumnPropertyAttribute(String Column)
        {
            this.Name = Column;
        }
    }
}
