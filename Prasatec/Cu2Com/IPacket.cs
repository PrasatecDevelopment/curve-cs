using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com
{
    public interface IPacket
    {
        Int32 Version { get; }
        Double Stamp { get; }
        String Purpose { get; }
        String[] Keys { get; }
        Object[] Elements { get; }
    }
}
