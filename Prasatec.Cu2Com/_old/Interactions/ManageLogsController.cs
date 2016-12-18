using Prasatec.Cu2Com.Data;
using Prasatec.Experience;
using Prasatec.Experience.DynamicWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prasatec.Raden;
using System.Windows.Forms;

namespace Prasatec.Cu2Com.Interactions
{
    public sealed class ManageLogsController : IControllerCollection<LogCollection, Log>
    {
        public LogCollection Collection { get; private set; }
        public IConnection Connection { get; private set; }
        public IWindowCollection Window { get; private set; }

        public ManageLogsController(IConnection Connection)
        {
            this.Connection = Connection;
            this.Collection = new Data.LogCollection();
            this.Window = new CollectionWindow() { Text = "Log Entry Collection" };

            this.Collection.ListRefreshed += Collection_ListRefreshed;

            this.Window.AllowExport = false;
            this.Window.AllowImport = false;
            this.Window.Columns = new string[]
            {
                "ID",
                "Stamp",
                "Type",
                "Size"
            };

            this.Window.AddCategory("type", "Log Type", Enum.GetNames(typeof(LogEventType)));
            this.Window.SetCategory("type", 0);
            
            this.Window.DeleteRow += Window_DeleteRow;
            this.Window.ModifyRow += Window_ModifyRow;
            this.Window.NewRow += Window_NewRow;
            this.Window.PageChanged += Window_PageChanged;
            this.Window.ViewRow += Window_ViewRow;
            this.Window.CategoryChanged += Window_CategoryChanged;
        }

        private void Window_CategoryChanged(object sender, CollectionCategoryChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Window_ViewRow(object sender, GenericEventArgs<int> e)
        {
            //throw new NotImplementedException();
        }

        private void Window_PageChanged(object sender, GenericEventArgs<int> e)
        {
            //throw new NotImplementedException();
        }

        private void Window_NewRow(object sender, EventArgs e)
        {
            EditLogController editor = new Interactions.EditLogController(Connection);
            editor.ShowCreator(Window);

        }

        private void Window_ModifyRow(object sender, GenericEventArgs<int> e)
        {
            //throw new NotImplementedException();
        }

        private void Window_DeleteRow(object sender, GenericArrayEventArgs<int> e)
        {
            //throw new NotImplementedException();
        }

        private void Collection_ListRefreshed(object sender, EventArgs e)
        {
        }

        public void Show()
        {
            this.Window.Show();
            //throw new NotImplementedException();
        }

        public void Show(IWin32Window owner)
        {
            this.Window.Show(owner);
            //throw new NotImplementedException();
        }
    }
}