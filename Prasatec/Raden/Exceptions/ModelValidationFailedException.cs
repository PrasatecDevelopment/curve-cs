using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class ModelValidationFailedException : Exception
    {
        public ModelValidationFailedException(string[] validationErrors)
            : base("This record could not be committed because of:\n\n" + String.Join("\n", validationErrors))
        {

        }
    }
}
