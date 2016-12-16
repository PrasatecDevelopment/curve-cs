using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeCurrencyAttribute : ValueTypeAttribute
    {
        public ValueTypeCurrencyAttribute()
            : base("Currency")
        {
        }
        public ValueTypeCurrencyAttribute(decimal Value)
            : base("Currency")
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
