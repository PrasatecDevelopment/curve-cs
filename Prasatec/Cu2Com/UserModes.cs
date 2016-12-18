using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com
{
    public enum UserModes : ushort
    {
        System = 0,
        Disabled = 1,
        User = 100,
        Supervisor = 1000,
        Consultant = 5000,
        Manager = 10000,
        Administrator = 30000,
        Executive = 50000,
        Superuser = 65535
    }
}
