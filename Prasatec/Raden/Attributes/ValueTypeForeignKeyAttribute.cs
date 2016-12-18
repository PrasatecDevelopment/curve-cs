using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeForeignKeyAttribute : ValueTypeAttribute
    {
        public ValueTypeForeignKeyAttribute()
            : base("ForeignKey")
        {
        }
        public ValueTypeForeignKeyAttribute(ulong Value)
            : base("ForeignKey")
        {
            this.setValue(Value);
        }

        public override object ConvertTo(object value)
        {
            ulong result = 0;
            if (value is IForeignKey)
            {
                result = ((IForeignKey)value).getValue();
            }
            return result;
        }

        public override bool Validate(object value)
        {
            throw new NotImplementedException();
        }
    }
}
