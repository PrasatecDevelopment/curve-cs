using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public abstract class ValueTypeAttribute : Attribute
    {
        public object Default { get; private set; }
        internal Boolean DefaultSet { get; private set; }

        public String TypeName { get; private set; }
        protected ValueTypeAttribute(string TypeName)
        {
            this.TypeName = TypeName;
        }
        protected void setValue(object value)
        {
            this.Default = value;
            this.DefaultSet = true;
        }

        public abstract Boolean Validate(object value);
        public virtual T ConvertTo<T>(object value)
        {
            T result;
            try
            {
                result = (T)Convert.ChangeType(value, typeof(T));
            }
            catch { result = default(T); }
            return result;
        }
        public virtual object ConvertTo(object value) { return value; }
    }
}
