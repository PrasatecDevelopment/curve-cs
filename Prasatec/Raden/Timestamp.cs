using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public struct Timestamp : ITimestamp
    {
        public static Timestamp FromDouble(object value, Type target)
        {
            return new Timestamp(DateTime.FromOADate(Convert.ToDouble(value)));
        }

        internal DateTime _value;
        public Timestamp(DateTime value)
        {
            this._value = value;

        }
        public Timestamp(Double value)
        {
            this._value = DateTime.FromOADate(value);
        }

        public static implicit operator Timestamp(DateTime value)
        {
            return new Timestamp(value);
        }
        public static implicit operator DateTime(Timestamp ts)
        {
            return ts._value;
        }
        public static implicit operator double(Timestamp ts)
        {
            return ts._value.ToOADate();
        }

        public override string ToString()
        {
            return _value.ToOADate().ToString();
        }
        public double ToDouble()
        {
            return _value.ToOADate();
        }

        public Boolean IsSet() { return _value != DateTime.MinValue; }
    }
}
