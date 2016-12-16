using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T Value { get; set; }
        public GenericEventArgs(T Value)
        {
            this.Value = Value;
        }
    }
}
