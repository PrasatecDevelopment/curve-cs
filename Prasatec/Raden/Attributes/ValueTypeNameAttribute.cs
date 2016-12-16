using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeNameAttribute : ValueTypeAttribute
    {
        public ValueTypeNameAttribute()
            : base("Name")
        {
        }
        public ValueTypeNameAttribute(String Value)
            : base("Name")
        {
            this.setValue(Value);
        }

        public new String ConvertTo(object value)
        {
            return value?.ToString();
        }

        public override bool Validate(object value)
        {
            if (value?.ToString().Length > 125) { return false; }
            return true;
        }
    }
}
