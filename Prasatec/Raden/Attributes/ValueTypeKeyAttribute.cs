using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeKeyAttribute : ValueTypeAttribute
    {
        public ValueTypeKeyAttribute()
            : base("Key")
        {
        }
        public ValueTypeKeyAttribute(String Value)
            : base("Key")
        {
            this.setValue(Value);
        }

        public new String ConvertTo(object value)
        {
            return value?.ToString();
        }

        public override bool Validate(object value)
        {
            if (value?.ToString().Length > 50) { return false; }
            return true;
        }
    }
}
