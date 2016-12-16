using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public enum MysqlConnectionFailedReasons
    {
        NoServer,
        NoUsername,
        NoDatabase,

        ConnectionFailed,
        EncryptionFailed,

        LoginFailed,
        NotSure
    }
}
