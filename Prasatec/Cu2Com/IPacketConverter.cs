using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com
{
    public interface IPacketConverter
    {
        Int32 Version { get; }
        String Purpose { get; set; }
        KeyValuePair<String, Object>[] Elements { get; }

        void SetElement(String Name, Object Value);
        Object GetElement(String Name);
        O GetElement<O>(String Name);
        void ClearElement(String Name);

        Byte[] Export();
        Byte[] Export(bool includeHeaders);
        String ExportToString();
        String ExportToString(bool includeHeaders);
        IPacketConverter Import(Byte[] Data);
    }
}
