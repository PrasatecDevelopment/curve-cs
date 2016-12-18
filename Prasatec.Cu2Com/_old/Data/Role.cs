using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Data
{
    [TableModelAttribute("users_roles", "roleId")]
    public sealed class Role : Model
    {
        public Role()
        {

        }
        #region " Model "
        [ColumnProperty("roleName")]
        [ValueTypeName()]
        public String Name { get; set; } // Name: 125
        [ColumnProperty("roleMode")]
        [ValueTypeEnum(typeof(RoleModes))]
        public RoleModes Mode { get; set; } // ListEnum
        [ColumnProperty("roleDescription")]
        [ValueTypeDescription()]
        public String Description { get; set; } // Description: 250
        #endregion
    }
}