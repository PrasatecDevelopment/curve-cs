using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class MysqlConnectionFailedException : DatabaseConnectionFailedException
    {
        public MysqlConnectionFailedException(MysqlConnectionFailedReasons Reason)
            : base("mysql", Enum.GetName(typeof(MysqlConnectionFailedReasons), Reason))
        {

        }
    }
}
