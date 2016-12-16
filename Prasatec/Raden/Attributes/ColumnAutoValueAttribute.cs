using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ColumnAutoValueAttribute : Attribute
    {
        public ColumnAutoValues Value { get; internal set; }
        public ColumnAutoValueAttribute(ColumnAutoValues Value)
        {
            if (Value == ColumnAutoValues.AutoIncrement)
            {
                throw new AutoIncrementAlreadySetException();
            }
            this.Value = Value;

        }
    }
}
