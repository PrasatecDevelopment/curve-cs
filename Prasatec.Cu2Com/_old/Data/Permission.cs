using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Data
{
    [TableModelAttribute("permissions", "permissionId")]
    public sealed class Permission : Model
    {
        internal Permission()
        {

        }
        #region " Model "
        [ColumnProperty("permissionKey")]
        [ValueTypeKey()]
        public String Key { get; set; } // Key: 50
        [ColumnProperty("permissionMode")]
        [ValueTypeEnum(typeof(RoleModes))]
        public RoleModes MinimumMode { get; set; } // EnumList
        [ColumnProperty("permissionDescription")]
        [ValueTypeDescription()]
        [ColumnAllowNull(true)]
        public String Description { get; set; } // Description: 250
        #endregion
    }
}
