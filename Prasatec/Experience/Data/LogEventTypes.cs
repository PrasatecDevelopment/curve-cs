using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Data
{
    public enum LogEventType
    {
        None = 0,
        [Description("Manual Entry")]
        Manual = 1
    }
}
