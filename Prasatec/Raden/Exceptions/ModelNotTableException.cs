using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ModelNotTableException : Exception
    {
        public ModelNotTableException(string modelName)
            : base(String.Format("'{0}' must implement IModel and have attribute TableModel before it can be used by RADEN", modelName))
        {

        }
    }
}
