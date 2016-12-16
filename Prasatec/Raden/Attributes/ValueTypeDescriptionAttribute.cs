using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeDescriptionAttribute : ValueTypeAttribute
    {
        public ValueTypeDescriptionAttribute()
            : base("Description")
        {
        }
        public ValueTypeDescriptionAttribute(string Value)
            : base("Description")
        {
            this.setValue(Value);
        }

        public new String ConvertTo(object value)
        {
            return value?.ToString();
        }

        public override bool Validate(object value)
        {
            if (value?.ToString().Length > 250) { return false; }
            return true;
        }
    }
}
