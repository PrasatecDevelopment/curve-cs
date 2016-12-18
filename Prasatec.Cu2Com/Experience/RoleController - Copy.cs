using Prasatec.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prasatec.Cu2Com.Raden;
using Prasatec.Raden;
using Prasatec.Experience.DynamicWindows;
using System.Windows.Forms;

namespace Prasatec.Cu2Com.Experience
{
    public class RoleController : IController<RoleModel, RoleBuilder, RoleCollection>
    {
        public IConnection Connection { get; private set; }

        public RoleController(IConnection Connection)
        {
            this.Connection = Connection;
        }

        #region " Database Roles "
        public RoleBuilder Create()
        {
            return new RoleBuilder(this);
        }

        public RoleBuilder Duplicate(RoleModel Source)
        {
            var result = new RoleBuilder(this);
            if (Source != null)
            {
                result = (RoleBuilder)result.Clone(Source);
            }
            return result;
        }

        public RoleBuilder Count()
        {
            return new RoleBuilder(this);
        }

        public RoleBuilder Count(RoleModel Source)
        {
            var result = new RoleBuilder(this);
            if (Source != null)
            {
                result = (RoleBuilder)result.Clone(Source);
            }
            return result;
        }

        public RoleBuilder Delete()
        {
            return new RoleBuilder(this);
        }

        public RoleBuilder Delete(RoleModel Source)
        {
            var result = new RoleBuilder(this);
            if (Source != null)
            {
                result = (RoleBuilder)result.Clone(Source);
            }
            return result;
        }

        public RoleBuilder Find()
        {
            return new RoleBuilder(this);
        }

        public RoleBuilder Find(RoleModel Source)
        {
            var result = new RoleBuilder(this);
            if (Source != null)
            {
                result = (RoleBuilder)result.Clone(Source);
            }
            return result;
        }

        public RoleBuilder Retrieve()
        {
            return new RoleBuilder(this);
        }

        public RoleBuilder Retrieve(RoleModel Source)
        {
            var result = new RoleBuilder(this);
            if (Source != null)
            {
                result = (RoleBuilder)result.Clone(Source);
            }
            return result;
        }

        public RoleBuilder Update()
        {
            return new RoleBuilder(this);
        }

        public RoleBuilder Update(RoleModel Source)
        {
            var result = new RoleBuilder(this);
            if (Source != null)
            {
                result = (RoleBuilder)result.Clone(Source);
            }
            return result;
        }
        #endregion

        #region " Editor/Viewer Window "
        private RoleModel editorModel;
        private IWindowEditor editorWindow;
        IQueryResult<RoleModel> editorResult;

        private void buildEditor(IWindowEditor Target)
        {
            string[] modenames = Enum.GetNames(typeof(UserModes));
            int modeindex = Array.IndexOf(modenames, Enum.GetName(typeof(UserModes), editorModel.Mode));



            Target.Fields.AddTextbox("vName", "Role Name", editorModel.Name);
            Target.Fields.AddContent("vDescription", "Description", editorModel.Description);
            Target.Fields.AddDropdown("vMode", "User Mode", modenames, modeindex);
        }

        public IQueryResult<RoleModel> ShowCreate(IWin32Window owner)
        {
            if (this.editorModel != null)
            {
                this.editorModel = null;
            }
            if (this.editorResult != null)
            {
                this.editorResult = null;
            }
            if (this.editorWindow != null)
            {
                this.editorWindow = null;
            }
            this.editorModel = new Raden.RoleModel();
            using (editorWindow = new EditorWindow())
            {
                this.buildEditor(editorWindow);
                editorWindow.Mode = DynamicEditorModes.Edit;
                editorWindow.CanChangeMode = false;
                editorWindow.Text = "Create New Role";
                editorWindow.Saved += EditorWindow_Saved;
                editorWindow.ShowDialog(owner);
            }

            return this.editorResult;
        }

        private void EditorWindow_Saved(object sender, GenericEventArgs<bool> e)
        {
            RoleBuilder builder;
            if (editorModel == null || editorModel.ID == 0)
            {
                builder = this.Create();
            }
            else
            {
                builder = this.Update(this.editorModel);
            }
            var result = builder
                .Name(editorWindow.Fields.GetValue<string>("vName"))
                .Description(editorWindow.Fields.GetValue<string>("vDescription"))
                .Mode((UserModes)Enum.GetValues(typeof(UserModes)).GetValue(editorWindow.Fields.GetValue<int>("vMode")))
                .Execute();
            
            e.Value = result.Successful;
            if (result.Successful == false)
            {
                MessageBox.Show((IWin32Window)editorWindow, result.ErrorMessage, "Error: " + editorWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.editorResult = result;
            }
        }

        public void ShowViewer(IWin32Window owner, ulong ID)
        {
            this.ShowViewer(owner, (RoleBuilder)this.Retrieve().ID(ID));
        }
        public void ShowViewer(IWin32Window owner, RoleBuilder Source)
        {
            var result = Source.Execute();
            RoleModel model = null;
            if (result.Records != null && result.Records.Length > 0)
            {
                model = result.Records[0];
            }
            result = null;
            this.ShowViewer(owner, model);
        }
        public void ShowViewer(IWin32Window owner, RoleModel Source)
        {
            if (this.editorModel != null)
            {
                this.editorModel = null;
            }
            if (this.editorResult != null)
            {
                this.editorResult = null;
            }
            if (this.editorWindow != null)
            {
                this.editorWindow = null;
            }
            this.editorModel = Source;
            using (editorWindow = new EditorWindow())
            {
                this.buildEditor(editorWindow);
                editorWindow.Mode = DynamicEditorModes.View;
                editorWindow.CanChangeMode = false;
                editorWindow.Text = "Viewing ROLE" + Source.ID.ToString();
                editorWindow.ShowDialog(owner);
            }
        }
        public IQueryResult<RoleModel> ShowEditor(IWin32Window owner, ulong ID)
        {
            return this.ShowEditor(owner, (RoleBuilder)this.Retrieve().ID(ID));
        }
        public IQueryResult<RoleModel> ShowEditor(IWin32Window owner, RoleBuilder Source)
        {
            var result = Source.Execute();
            RoleModel model = null;
            if (result.Records != null && result.Records.Length > 0)
            {
                model = result.Records[0];
            }
            result = null;
            return this.ShowEditor(owner, model);
        }
        public IQueryResult<RoleModel> ShowEditor(IWin32Window owner, RoleModel Source)
        {
            if (this.editorModel != null)
            {
                this.editorModel = null;
            }
            if (this.editorResult != null)
            {
                this.editorResult = null;
            }
            if (this.editorWindow != null)
            {
                this.editorWindow = null;
            }
            this.editorModel = Source;
            using (editorWindow = new EditorWindow())
            {
                this.buildEditor(editorWindow);
                editorWindow.Mode = DynamicEditorModes.Edit;
                editorWindow.CanChangeMode = false;
                editorWindow.Text = "Editing ROLE" + Source.ID.ToString();
                editorWindow.Saved += EditorWindow_Saved;
                editorWindow.ShowDialog(owner);
            }

            return this.editorResult;
        }
        #endregion

        #region " Collection Window "
        private RoleCollection collectionObject;
        private IWindowCollection collectionWindow;

        public void ShowCollection()
        {
            this.ShowCollection(this.Retrieve().GetCollection());
        }
        public void ShowCollection(RoleBuilder Source)
        {
            this.ShowCollection(Source.GetCollection());
        }
        public void ShowCollection(RoleCollection Source)
        {
            if (this.collectionObject != null)
            {
                this.collectionObject = null;
            }
            if (this.collectionWindow != null)
            {
                this.collectionWindow = null;
            }
            this.collectionObject = Source;
            this.collectionWindow = new CollectionWindow() { Text = "Role Collection" };
            collectionObject.Refresh();

            ((CollectionWindow)this.collectionWindow).Show();
            this.collectionWindow.AllowImport = false;
            this.collectionWindow.AllowExport = false;
            this.collectionWindow.Columns = collectionObject.Columns;
            foreach (var item in collectionObject.Records)
            {
                collectionWindow.AddRow(item.Name, item.Mode);
            }

            this.collectionWindow.NewRow += CollectionWindow_NewRow;
            this.collectionWindow.DeleteRow += CollectionWindow_DeleteRow;
            this.collectionWindow.ModifyRow += CollectionWindow_ModifyRow;
            this.collectionWindow.ViewRow += CollectionWindow_ViewRow;

            collectionObject.ListRefreshed += CollectionObject_ListRefreshed;

            //throw new NotImplementedException();
        }

        private void CollectionWindow_ViewRow(object sender, GenericEventArgs<int> e)
        {
            this.ShowViewer(collectionWindow, collectionObject.Records[e.Value].ID);
        }

        private void CollectionWindow_ModifyRow(object sender, GenericEventArgs<int> e)
        {
            this.ShowEditor(collectionWindow, collectionObject.Records[e.Value].ID);
            this.collectionObject.Refresh();
        }

        private void CollectionWindow_DeleteRow(object sender, GenericArrayEventArgs<int> e)
        {
            foreach (ulong id in e.Value)
            {
                this.Delete().ID(collectionObject.Records[id].ID).Execute();
            }
            this.collectionObject.Refresh();
        }

        private void CollectionObject_ListRefreshed(object sender, EventArgs e)
        {
            collectionWindow.ClearRows();
            foreach (var item in collectionObject.Records)
            {
                collectionWindow.AddRow(item.Name, item.Mode);
            }
        }

        private void CollectionWindow_NewRow(object sender, EventArgs e)
        {
            if (this.ShowCreate(collectionWindow).Successful == true)
            {
                this.collectionObject.Refresh();
            }
        }
        #endregion
    }
}
