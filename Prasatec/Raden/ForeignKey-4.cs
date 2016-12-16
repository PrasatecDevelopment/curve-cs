using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public struct ForeignKey<T1, T2, T3, T4> : IForeignKey
        where T1 : IModel
    {
        private int _value;

        public ForeignKey(int value)
        {
            this._value = value;
        }

        int IForeignKey.getValue()
        {
            return this._value;
        }

        public static implicit operator ForeignKey<T1, T2, T3, T4>(Int32 value)
        {
            return ForeignKeyHelper.Convert<Int32, ForeignKey<T1, T2, T3, T4>>(value);
        }
        public static implicit operator int(ForeignKey<T1, T2, T3, T4> value)
        {
            return ForeignKeyHelper.Convert<ForeignKey<T1, T2, T3, T4>, Int32>(value);
        }
        public static implicit operator ForeignKey<T1, T2, T3, T4>(T1 value)
        {
            return ForeignKeyHelper.Convert<T1, ForeignKey<T1, T2, T3, T4>>(value);
        }
    }
}
