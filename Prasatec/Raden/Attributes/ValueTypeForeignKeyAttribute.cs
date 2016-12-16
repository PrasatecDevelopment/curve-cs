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
        public ValueTypeForeignKeyAttribute(Int32 Value)
            : base("ForeignKey")
        {
            this.setValue(Value);
        }

        public override object ConvertTo(object value)
        {
            Int32 result = -1;
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
