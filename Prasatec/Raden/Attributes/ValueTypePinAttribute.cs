using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypePinAttribute : ValueTypeAttribute
    {
        public ValueTypePinAttribute()
            : base("Pin")
        {
        }
        public ValueTypePinAttribute(string Value)
            : base("Pin")
        {
            this.setValue(Value);
        }

        public new String ConvertTo(object value)
        {
            return value?.ToString();
        }

        public override bool Validate(object value)
        {
            if (value?.ToString().Length > 4) { return false; }
            int dummy = 0;
            if (Int32.TryParse(value?.ToString(), out dummy) == false) { return false; }
            return true;
        }
    }
}
