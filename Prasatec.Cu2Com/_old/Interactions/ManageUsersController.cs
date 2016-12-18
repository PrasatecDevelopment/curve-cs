using Prasatec.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Cu2Com.Interactions
{
    public sealed class ManageUsersController
    {
        private IWindowCollection Window { get; set; }

        public Boolean Visible { get { return Window.Visible; } }

        public ManageUsersController()
        {
            Window = new Prasatec.Experience.DynamicWindows.CollectionWindow() { Text = "User Collection" };
            Window.AllowImport = false;
            Window.AllowExport = false;

            Window.NewRow += Window_NewRow;
        }

        private void Window_NewRow(object sender, EventArgs e)
        {
            var manager = EditUserController.OpenCreate();
            manager.Show(this.Window);
        }

        public void Show()
        {
            Window.Show();
        }
        public void Show(IWin32Window owner)
        {
            Window.Show(owner);
        }
    }
}
