using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ColumnIndexAttribute : Attribute
    {
        public ColumnIndexTypes Value { get; internal set; }
        public ColumnIndexAttribute(ColumnIndexTypes Value)
        {
            if (Value == ColumnIndexTypes.Primary)
            {
                throw new PrimaryKeyAlreadyDefinedException();
            }
            this.Value = Value;
        }
    }
}
