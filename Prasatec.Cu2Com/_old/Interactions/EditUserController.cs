using Prasatec.Cu2Com.Data;
using Prasatec.Experience;
using Prasatec.Experience.DynamicWindows;
using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Cu2Com.Interactions
{
    public sealed class EditUserController
    {
        private EditUserController()
        {

        }
        public User Model { get; private set; }
        private IWindowEditor Window { get; set; }

        private static void createFields(IWindowEditor Window, User Model)
        {
            object[] roles = new object[0];
            Window.Fields.AddTextbox("login", "User ID", Model.Login);
            Window.Fields.AddNumber("code", "User PIN", 1000, 9999, Model.Code);
            Window.Fields.AddTextbox("email", "Email Address", Model.Email);
            Window.Fields.AddTextbox("name", "Display Name", Model.Name);
            Window.Fields.AddDropdown("role", "User Role", roles, -1);
        }

        public static EditUserController OpenCreate()
        {
            var result = new EditUserController();
            result.Model = new User();
            result.Window = new EditorWindow();
            //result.Window.EditTitle = "Create New User";
            result.Window.Mode = DynamicEditorModes.Edit;
            result.Window.CanChangeMode = false;
            createFields(result.Window, result.Model);
            result.mapEvents();
            return result;
        }
        public static EditUserController OpenView(User Model, bool AllowEditing)
        {
            var result = new EditUserController();
            result.Model = Model;
            result.Window = new EditorWindow();
            //result.Window.EditTitle = "Edit User ID " + Model.ID.ToString();
            //result.Window.ViewTitle = "View User ID " + Model.ID.ToString();
            result.Window.Mode = DynamicEditorModes.View;
            result.Window.CanChangeMode = AllowEditing;
            createFields(result.Window, result.Model);
            result.mapEvents();
            return result;
        }
        public static EditUserController openModify(User Model, bool AutomaticallyClose)
        {
            var result = new EditUserController();
            result.Model = Model;
            result.Window = new EditorWindow();
            //result.Window.EditTitle = "Edit User ID " + Model.ID.ToString();
            //result.Window.ViewTitle = "View User ID " + Model.ID.ToString();
            result.Window.Mode = DynamicEditorModes.Edit;
            result.Window.CanChangeMode = !AutomaticallyClose;
            createFields(result.Window, result.Model);
            result.mapEvents();
            return result;
        }

        private void mapEvents()
        {

        }

        public void Show(IWin32Window owner)
        {
            ((EditorWindow)Window).ShowDialog(owner);
        }
    }
}
