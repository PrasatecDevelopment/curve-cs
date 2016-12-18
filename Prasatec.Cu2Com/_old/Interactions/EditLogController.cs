using Prasatec.Cu2Com.Data;
using Prasatec.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prasatec.Raden;
using System.Windows.Forms;
using Prasatec.Experience.DynamicWindows;

namespace Prasatec.Cu2Com.Interactions
{
    public sealed class EditLogController : IControllerEditor<Log>
    {
        public EditLogController(IConnection Connection)
        {
            this.Connection = Connection;
        }
        public IConnection Connection { get; private set; }
        public Log Model { get; private set; }
        public IWindowEditor Window { get; private set; }
        public Boolean Successful { get; private set; }

        private void createWindow(string Title)
        {
            if (this.Window != null)
            {
                this.Window.Saved -= Window_Saved;
                this.Window = null;
            }
            this.Successful = false;
            this.Window = new EditorWindow() { Text = Title };
            this.Window.Fields.AddNumber("eventId", "Event ID", 0, int.MaxValue, 0);
            this.Window.Fields.AddTextbox("eventCode", "Event Code", "");
            this.Window.Fields.AddDropdown("eventType", "Event Type", Enum.GetNames(typeof(LogEventType)), 0);
            this.Window.Fields.AddContent("eventContent", "Content", "");
            this.Window.Saved += Window_Saved;
        }

        private void Window_Saved(object sender, GenericEventArgs<bool> e)
        {
            if (this.Model == null)
            {
                this.Model = new Log();
            }
            this.Model.EventID = Window.Fields.GetValue<int>("eventId");
            this.Model.Code = Window.Fields.GetValue<string>("eventCode");
            this.Model.Type = (LogEventType)Enum.GetValues(typeof(LogEventType)).GetValue(Window.Fields.GetValue<int>("eventType"));
            this.Model.Event = Window.Fields.GetValue<string>("eventContent");

            IQueryResult<Log> result;
            if (Model.ID == 0)
            {
                result = Connection.Create<Log>(Model);
                Model.ID = result.InsertId;
            }
            else
            {
                IQuery query = new QueryBuilder<Data.Log>()
                    .Where(x => x.ID, Model.ID)
                    .Build();
                result = Connection.Update<Log>(query, Model);
            }
            e.Value = result.Successful;
        }

        public bool ShowCreator(IWin32Window owner)
        {
            this.createWindow("Create Log Entry");
            this.Window.Mode = DynamicEditorModes.Edit;
            this.Window.CanChangeMode = false;
            this.Window.ShowDialog(owner);
            return false;
        }

        public bool ShowEditor(IModel Model, IWin32Window owner)
        {
            throw new NotImplementedException();
        }

        public bool ShowViewer(IModel Model, IWin32Window owner)
        {
            throw new NotImplementedException();
        }
    }
}
