using Prasatec.Experience;
using System;
using Prasatec.Cu2Com.Raden;
using Prasatec.Raden;
using Prasatec.Experience.DynamicWindows;

namespace Prasatec.Cu2Com.Experience
{
    public class EventController : BaseController<EventModel, EventCollection, EventBuilder>
    {
        public EventController(IConnection Connection) : base(Connection)
        {
        }

        protected override string TitleCreate { get { return "Create New Event"; } }
        protected override string TitleEdit { get { return "Edit Event #"; } }
        protected override string TitleView { get { return "View Event #"; } }

        protected override IWindowCollection CreateCollection()
        {
            return new CollectionWindow() { Text = "Event Collection" };
        }

        protected override IWindowEditor CreateEditor()
        {
            var result = new EditorWindow();

            string[] typeNames = Enum.GetNames(typeof(EventTypes));
            int typeIndex = Array.IndexOf(typeNames, Enum.GetName(typeof(EventTypes), editorModel.Type));

            result.Fields.AddTextbox("vCode", "Event Code", editorModel.Code);
            result.Fields.AddDropdown("vType", "Event Type", typeNames, typeIndex);
            result.Fields.AddContent("vContent", "Content", editorModel.Content);

            return result;
        }

        protected override EventModel CreateModel()
        {
            return new Raden.EventModel();
        }

        protected override void ParseRow(EventModel item)
        {
            if (collectionWindow == null) { return; }
            collectionWindow.AddRow(item.Code, Enum.GetName(typeof(EventTypes), item.Type));
        }

        protected override EventBuilder PrepareBuilder(EventBuilder builder)
        {
            builder = builder
                .Code(editorWindow.Fields.GetValue<string>("vCode"))
                .Content(editorWindow.Fields.GetValue<string>("vContent"))
                .Type((EventTypes)Enum.GetValues(typeof(EventTypes)).GetValue(editorWindow.Fields.GetValue<int>("vType")));
            return builder;
        }
    }
}
