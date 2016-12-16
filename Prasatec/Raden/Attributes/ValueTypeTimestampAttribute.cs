using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeTimestampAttribute : ValueTypeAttribute
    {
        public ValueTypeTimestampAttribute()
            : base("Timestamp")
        {
        }
        public ValueTypeTimestampAttribute(DateTime Value)
            : base("Timestamp")
        {
            this.setValue(Value.ToOADate());
        }

        public override object ConvertTo(object value)
        {
            double dbl = 0;
            DateTime result;
            if (Double.TryParse(value.ToString(), out dbl))
            {
                result = DateTime.FromOADate(dbl);
            }
            else { result = DateTime.MinValue; }

            return result.ToOADate();
        }

        public override bool Validate(object value)
        {
            throw new NotImplementedException();
        }
    }
}
