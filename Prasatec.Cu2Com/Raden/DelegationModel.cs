using Prasatec.Raden;
using System;
using System.Reflection;

namespace Prasatec.Cu2Com.Raden
{
    [TableModelAttribute("users_delegations", "delegation_id")]
    public sealed class DelegationModel : BaseModel<DelegationModel>
    {
        private ulong v_ID;
        private ForeignKey<UserModel> v_Manager, v_Subordinate;

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
            ColumnProperty("subordinate_id"),
            ValueTypeForeignKey()
        ]
        public ForeignKey<UserModel> Subordinate
        {
            get { return v_Subordinate; }
            set
            {
                if (v_Subordinate == value) { return; }
                v_Subordinate = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
