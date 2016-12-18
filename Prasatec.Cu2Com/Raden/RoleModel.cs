using Prasatec.Raden;
using System;
using System.Reflection;

namespace Prasatec.Cu2Com.Raden
{
    [TableModelAttribute("users_roles", "role_id")]
    public sealed class RoleModel : BaseModel<RoleModel>
    {
        public RoleModel()
        {
        }

        private ulong i_ID;
        private string s_Name, s_Description;
        private UserModes e_Mode;

        public override ulong ID
        {
            get { return i_ID; }
            set
            {
                if (i_ID == value) { return; }
                i_ID = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("role_name"),
            ValueTypeName()
        ]
        public String Name
        {
            get { return s_Name; }
            set
            {
                if (s_Name == value) { return; }
                this.s_Name = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("role_description"),
            ValueTypeDescription(),
            ColumnAllowNull(true)
        ]
        public String Description
        {
            get { return s_Description; }
            set
            {
                if (s_Description == value) { return; }
                this.s_Description = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("role_usermode"),
            ValueTypeEnum(typeof(UserModes), 0)
        ]
        public UserModes Mode
        {
            get { return e_Mode; }
            set
            {
                if (e_Mode == value) { return; }
                this.e_Mode = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
