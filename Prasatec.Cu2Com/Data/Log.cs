using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Data
{
    [TableModelAttribute("logs", "eventId")]
    public sealed class Log : Model
    {
        public Log()
        {

        }
        #region " Model "
        [ColumnProperty("eventIdentifier")]
        [ValueTypeInteger(0)]
        public Int32 EventID { get; set; } = 0; // Integer
        [ColumnProperty("eventCode")]
        [ValueTypeKey()]
        [ColumnAllowNull(true)]
        public String Code { get; set; } // Key: 50
        [ColumnProperty("eventType")]
        [ValueTypeEnum(typeof(LogEventType))]
        [ColumnAllowNull(true)]
        public LogEventType Type { get; set; } // EnumList
        [ColumnProperty("eventContent")]
        [ValueTypeContent()]
        public String Event { get; set; }  // Content 65535
        #endregion
    }
}