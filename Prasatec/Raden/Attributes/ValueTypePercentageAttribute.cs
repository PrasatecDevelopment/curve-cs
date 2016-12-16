using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypePercentageAttribute : ValueTypeAttribute
    {
        public ValueTypePercentageAttribute()
            : base("Percentage")
        {
        }
        public ValueTypePercentageAttribute(float Value)
            : base("Percentage")
        {
            this.setValue(Value);
        }

        public new float ConvertTo(object value)
        {
            throw new NotImplementedException();
        }

        public override bool Validate(object value)
        {
            throw new NotImplementedException();
        }
    }
}
