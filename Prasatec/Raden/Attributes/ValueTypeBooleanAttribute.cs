using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeBooleanAttribute : ValueTypeAttribute
    {
        public ValueTypeBooleanAttribute()
            : base("Boolean")
        {
        }
        public ValueTypeBooleanAttribute(Boolean Value)
            : base("Boolean")
        {
            this.setValue(Value);
        }

        public new Boolean ConvertTo(object value)
        {
            var result = false;
            bool.TryParse(value?.ToString(), out result);
            return result;
        }

        public override bool Validate(object value)
        {
            bool test;
            return bool.TryParse(value?.ToString(), out test);
        }
    }
}
