using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ValueTypeEnumAttribute : ValueTypeAttribute
    {
        private Type enumType;
        public ValueTypeEnumAttribute(Type Enum)
            : base("Enum")
        {
            this.enumType = Enum;
        }
        public ValueTypeEnumAttribute(Type Enum, object Value)
            : this(Enum)
        {
            this.setValue(Value);
        }

        public new int ConvertTo(object value)
        {
            throw new NotImplementedException();
        }

        public override bool Validate(object value)
        {
            throw new NotImplementedException();
        }
    }
}
