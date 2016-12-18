using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Data
{
    [TableModelAttribute("settings", "settingId")]
    public sealed class Setting : Model
    {
        public Setting()
        {

        }
        #region " Model "
        [ColumnProperty("settingName")]
        [ValueTypeKey()]
        public String Key { get; set; } // Key: 50
        [ColumnProperty("settingValue")]
        [ValueTypeDescription()]
        [ColumnAllowNull(true)]
        public Object Value { get; set; } // Description: 250
        [ColumnProperty("settingDescription")]
        [ValueTypeDescription()]
        [ColumnAllowNull(true)]
        public String Description { get; set; } // Description: 250
        #endregion
    }
}