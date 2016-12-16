using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class DatabaseEngineNotImplementedException : Exception
    {
        public DatabaseEngineNotImplementedException(DatabaseEngines Engine)
            : base("The database engine selected has not yet been implemented: " + Enum.GetName(typeof(DatabaseEngines), Engine))
        {

        }
    }
}
