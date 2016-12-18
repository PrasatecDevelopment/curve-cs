using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public struct ForeignKey<T1, T2> : IForeignKey
        where T1 : IModel
    {
        private ulong _value;

        public ForeignKey(ulong value)
        {
            this._value = value;
        }

        ulong IForeignKey.getValue()
        {
            return this._value;
        }

        public static implicit operator ForeignKey<T1, T2>(ulong value)
        {
            return ForeignKeyHelper.Convert<ulong, ForeignKey<T1, T2>>(value);
        }
        public static implicit operator ulong(ForeignKey<T1, T2> value)
        {
            return ForeignKeyHelper.Convert<ForeignKey<T1, T2>, ulong>(value);
        }
        public static implicit operator ForeignKey<T1, T2>(T1 value)
        {
            return ForeignKeyHelper.Convert<T1, ForeignKey<T1, T2>>(value);
        }
    }
}
