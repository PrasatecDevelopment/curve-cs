using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Data
{
    [TableModelAttribute("users_reset", "resetId")]
    public sealed class UserReset : Model
    {
        public UserReset()
        {

        }
        #region " Model "
        [ColumnProperty("userId")]
        [ValueTypeForeignKey()]
        public ForeignKey<User> User { get; set; } // ForeignKey
        [ColumnProperty("emailAddress")]
        [ValueTypeDescription()]
        public String Email { get; set; } // Description: 250
        [ColumnProperty("resetToken")]
        [ValueTypeDescription()]
        public String Token { get; set; } // Description: 250
        [ColumnProperty("resetExpires")]
        [ValueTypeTimestamp()]
        public Timestamp Expires { get; set; } // Timestamp
        #endregion
    }
}
