using Prasatec.Raden;
using System;
using System.Reflection;

namespace Prasatec.Cu2Com.Raden
{
    [TableModelAttribute("users", "user_id")]
    public sealed class UserModel : BaseModel<UserModel>
    {
        private ulong v_ID;
        private string v_Login, v_Email, v_Name, v_CreatedMethod;
        private ushort v_Pin;
        private ForeignKey<RoleModel> v_Role;
        private ForeignKey<UserModel> v_Manager, v_CreatedBy;
        private Timestamp v_CreatedAt;

        public override ulong ID
        {
            get { return v_ID; }
            set
            {
                if (v_ID == value) { return; }
                v_ID = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("user_login"),
            ValueTypeName()
        ]
        public string Login
        {
            get { return v_Login; }
            set
            {
                if (v_Login == value) { return; }
                v_Login = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("user_pin"),
            ValueTypePin(),
            ColumnAllowNull(true)
        ]
        public ushort Pin
        {
            get { return v_Pin; }
            set
            {
                if (v_Pin == value) { return; }
                v_Pin = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("user_email"),
            ValueTypeName(),
            ColumnAllowNull(true)
        ]
        public string Email
        {
            get { return v_Email; }
            set
            {
                if (v_Email == value) { return; }
                v_Email = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("user_name"),
            ValueTypeName()
        ]
        public string Name
        {
            get { return v_Name; }
            set
            {
                if (v_Name == value) { return; }
                v_Name = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("role_id"),
            ValueTypeForeignKey()
        ]
        public ForeignKey<RoleModel> Role
        {
            get { return v_Role; }
            set
            {
                if (v_Role == value) { return; }
                v_Role = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("manager_id"),
            ValueTypeForeignKey()
        ]
        public ForeignKey<UserModel> Manager
        {
            get { return v_Manager; }
            set
            {
                if (v_Manager == value) { return; }
                v_Manager = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("created_stamp"),
            ValueTypeTimestamp(),
            ColumnAutoValue(ColumnAutoValues.CreatedStamp)
        ]
        public Timestamp CreatedAt
        {
            get { return v_CreatedAt; }
            set
            {
                if (v_CreatedAt == value) { return; }
                v_CreatedAt = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("created_by"),
            ValueTypeForeignKey(),
            ColumnAutoValue(ColumnAutoValues.CreatedUser)
        ]
        public ForeignKey<UserModel> CreatedBy
        {
            get { return v_CreatedBy; }
            set
            {
                if (v_CreatedBy == value) { return; }
                v_CreatedBy = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("created_method"),
            ValueTypeDescription()
        ]
        public String CreatedMethod
        {
            get { return v_CreatedMethod; }
            set
            {
                if (v_CreatedMethod == value) { return; }
                v_CreatedMethod = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
