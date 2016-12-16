using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string propertyName, string modelName, string tableName)
            : base(String.Format("The property '{0}' on model {1} (table: {2}) does not exist",
                propertyName, modelName, tableName))
        {

        }
    }
}
