using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Converters
{
    public sealed class JsonConverter : IPacketConverter
    {
        public JsonConverter()
        {

        }

        public KeyValuePair<string, object>[] Elements
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Purpose
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Version
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ClearElement(string Name)
        {
            throw new NotImplementedException();
        }

        public byte[] Export()
        {
            throw new NotImplementedException();
        }

        public byte[] Export(bool includeHeaders)
        {
            throw new NotImplementedException();
        }

        public string ExportToString()
        {
            throw new NotImplementedException();
        }

        public string ExportToString(bool includeHeaders)
        {
            throw new NotImplementedException();
        }

        public object GetElement(string Name)
        {
            throw new NotImplementedException();
        }

        public O GetElement<O>(string Name)
        {
            throw new NotImplementedException();
        }

        public IPacketConverter Import(byte[] Data)
        {
            throw new NotImplementedException();
        }

        public void SetElement(string Name, object Value)
        {
            throw new NotImplementedException();
        }
    }
}
