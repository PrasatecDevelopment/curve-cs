using Prasatec.Raden;
using System;
using System.Reflection;

namespace Prasatec.Cu2Com.Raden
{
    [TableModelAttribute("events", "event_id")]
    public sealed class EventModel : BaseModel<EventModel>
    {
        private ulong v_ID;
        private string v_Code, v_Content;
        private EventTypes v_Type;
        private Timestamp v_CreatedAt;
        private ForeignKey<UserModel> v_CreatedBy;

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
            ColumnProperty("event_code"),
            ValueTypeKey(),
            ColumnAllowNull(true)
        ]
        public string Code
        {
            get { return v_Code; }
            set
            {
                if (v_Code == value) { return; }
                v_Code = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("event_type"),
            ValueTypeEnum(typeof(EventTypes), 0)
        ]
        public EventTypes Type
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
            ColumnProperty("event_content"),
            ValueTypeContent()
        ]
        public string Content
        {
            get { return v_Content; }
            set
            {
                if (v_Content == value) { return; }
                v_Content = value;
                this.setChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [
            ColumnProperty("event_created"),
            ValueTypeForeignKey(),
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
            ColumnProperty("event_creator"),
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

        public override string ToString()
        {
            return Type.ToString() + " (" + Code + ")";
        }
    }
}
