using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal static class ForeignKeyHelper
    {
        internal static T Convert<F, T>(F value)
        {
            if (!(typeof(F).GetInterfaces().Contains(typeof(IForeignKey)) ||
                typeof(T).GetInterfaces().Contains(typeof(IForeignKey))))
            {
                throw new InvalidCastException(String.Format("{0} cannot be converted to {1} using this method.", typeof(F).FullName, typeof(T).FullName));
            }
            T result;
            int realValue;
            if (typeof(F) == typeof(int) && Int32.TryParse(value?.ToString(), out realValue))
            {
                result = (T)Activator.CreateInstance(typeof(T), realValue);
            }
            else if (typeof(F) == typeof(IModel) && Int32.TryParse(((IModel)value)?.ID.ToString(), out realValue))
            {
                result = (T)Activator.CreateInstance(typeof(T), realValue);
            }
            else if (typeof(T) == typeof(int))
            {
                result = (T)System.Convert.ChangeType(((IForeignKey)value).getValue(), typeof(T));
            }
            else
            {
                throw new InvalidCastException(String.Format("{0} cannot be converted to {1}", typeof(F).FullName, typeof(T).FullName));
            }
            return result;
        }
    }
}
