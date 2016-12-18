using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace Prasatec.Cu2Com.Raden
{
    [TableModelAttribute("users_roles", "role_id")]
    public sealed class RoleModel : IModel
    {
        private List<string> changedProperties;
        public RoleModel()
        {
            this.changedProperties = new List<string>();
        }
        public bool IsChanged(Expression<Func<RoleModel, object>> expr)
        {
            return this.changedProperties.Contains(C2CGlobals.def.GetProperty<RoleModel>(expr).Reference.Name);
        }
        private void setChanged(string Name)
        {
            if (Name.StartsWith("set_"))
            {
                Name = Name.Substring(Name.IndexOf("_") + 1);
                if (!this.changedProperties.Contains(Name))
                {
                    this.changedProperties.Add(Name);
                }
            }
        }

        private ulong i_ID;
        private string s_Name, s_Description;
        private UserModes e_Mode;

        public ulong ID
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
    }
}
