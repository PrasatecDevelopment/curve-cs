using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Data
{
    [TableModelAttribute("users", "userId")]
    public sealed class UserSecure : Model
    {
        internal UserSecure()
        {

        }
        #region " Model "
        [ColumnProperty("userLogin")]
        [ValueTypeName()]
        public String Login { get; set; } // Name: 125
        [ColumnProperty("userEmail")]
        [ValueTypeDescription()]
        [ColumnAllowNull(true)]
        public String Email { get; set; } // Description: 250
        [ColumnProperty("userName")]
        [ValueTypeDescription()]
        [ColumnAllowNull(true)]
        public String Name { get; set; } // Description: 250
        [ColumnProperty("userRole")]
        [ValueTypeForeignKey()]
        public ForeignKey<Role> Role { get; set; } // ForeignKey
        #endregion
    }
}