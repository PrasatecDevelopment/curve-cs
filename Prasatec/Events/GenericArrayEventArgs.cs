using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec
{
    public class GenericArrayEventArgs<T> : EventArgs
    {
        public T[] Value { get; set; }
        public GenericArrayEventArgs(T[] Value)
        {
            this.Value = Value;
        }
    }
}
