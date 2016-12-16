using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeIntegerAttribute : ValueTypeAttribute
    {
        public ValueTypeIntegerAttribute()
            : base("Integer")
        {
        }
        public ValueTypeIntegerAttribute(Int32 Value)
            : base("Integer")
        {
            this.setValue(Value);
        }

        public new int ConvertTo(object value)
        {
            throw new NotImplementedException();
        }

        public override bool Validate(object value)
        {
            throw new NotImplementedException();
        }
    }
}
