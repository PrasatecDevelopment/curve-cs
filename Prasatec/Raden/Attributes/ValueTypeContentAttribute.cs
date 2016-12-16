using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeContentAttribute : ValueTypeAttribute
    {
        public ValueTypeContentAttribute()
            : base("Content")
        {
        }
        public ValueTypeContentAttribute(string Value)
            : base("Content")
        {
            this.setValue(Value);
        }

        public new String ConvertTo(object value)
        {
            return value?.ToString();
        }

        public override bool Validate(object value)
        {
            if (value?.ToString().Length > 65535) { return false; }
            return true;
        }
    }
}
