using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class DatabaseConnectionFailedException : Exception
    {
        public DatabaseConnectionFailedException()
            : base("Could not connect to the database, Reason: NotSure")
        {

        }
        public DatabaseConnectionFailedException(string Engine, string Reason)
            : base("Could not connect to the " + Engine + " database, Reason: " + Reason)
        {

        }
    }
}
