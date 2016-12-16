using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class MissingDefinitionIdentifierException : Exception
    {
        public MissingDefinitionIdentifierException(string IdentifierType, string Identifier)
            : base(IdentifierType + " identifier '" + Identifier + "' has not yet been defined.")
        {

        }
    }
}
