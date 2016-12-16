using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec
{
    public interface IBuilder<T>
    {
        T Build();

        bool IsLocked { get; }
        void Lock();

        IBuilder<T> Save();
        IBuilder<T> Load();
    }
}
