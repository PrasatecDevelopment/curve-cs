using Prasatec.Raden;
using System;
using System.Reflection;

namespace Prasatec.Cu2Com.Raden
{
    [TableModelAttribute("users_actions", "action_id")]
    public sealed class ActionModel : BaseModel<ActionModel>
    {
        private ulong v_ID;
        private ForeignKey<UserModel> v_User, v_PerformedBy;
        private Timestamp v_Expires, v_PerformedAt;
        private ActionTypes v_Type;
        private String v_Data, v_PerformedMethod;

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
            ColumnProperty("user_id"),
            ValueTypeForeignKey()
        ]
        public ForeignKey<UserModel> User
        {
            get { return v_User; }
            set
            {
                if (v_User == value) { return; }
                v_User = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("action_type"),
            ValueTypeEnum(typeof(ActionTypes))
        ]
        public ActionTypes Type
        {
            get { return v_Type; }
            set
            {
                if (v_Type == value) { return; }
                v_Type = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("action_data"),
            ValueTypeContent(),
            ColumnAllowNull(true)
        ]
        public String Data
        {
            get { return v_Data; }
            set
            {
                if (v_Data == value) { return; }
                v_Data = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("action_expires"),
            ValueTypeTimestamp()
        ]
        public Timestamp Expires
        {
            get { return v_Expires; }
            set
            {
                if (v_Expires == value) { return; }
                v_Expires = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("performed_by"),
            ValueTypeForeignKey()
        ]
        public ForeignKey<UserModel> PerformedBy
        {
            get { return v_PerformedBy; }
            set
            {
                if (v_PerformedBy == value) { return; }
                v_PerformedBy = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("performed_at"),
            ValueTypeTimestamp()
        ]
        public Timestamp PerformedAt
        {
            get { return v_PerformedAt; }
            set
            {
                if (v_PerformedAt == value) { return; }
                v_PerformedAt = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("performed_method"),
            ValueTypeDescription(),
            ColumnAllowNull(true)
        ]
        public String PerformedMethod
        {
            get { return v_PerformedMethod; }
            set
            {
                if (v_PerformedMethod == value) { return; }
                v_PerformedMethod = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
