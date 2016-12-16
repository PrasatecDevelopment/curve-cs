using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class PropertyNotColumnException : Exception
    {
        public PropertyNotColumnException(string propertyName, string modelName, string tableName)
            : base(String.Format("'{0}' on model {1} (table: {2}) must have the attributes ColumnProperty and ValueType before it can be used by RADEN",
                propertyName, modelName, tableName))
        {

        }
    }
}
