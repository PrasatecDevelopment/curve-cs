using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ColumnAllowNullAttribute : Attribute
    {
        public Boolean Value { get; private set; }
        public ColumnAllowNullAttribute(Boolean Value)
        {
            this.Value = Value;
        }
    }
}
