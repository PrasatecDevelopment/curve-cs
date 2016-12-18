using Prasatec.Experience;
using Prasatec.Raden;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Cu2Com.Experience
{
    /// <summary>
    /// Base for data controllers
    /// </summary>
    /// <typeparam name="M">The model that is being interacted with</typeparam>
    /// <typeparam name="C">The collection that is used by this controller</typeparam>
    /// <typeparam name="B">Maps back to this builder</typeparam>
    public abstract class BaseController<M, C, B> : IController<M, C, B>
        where M : IModel
        where C : ICollection<M>
        where B : IControllerBuilder<M, C, B>
    {
        public IConnection Connection { get; private set; }
        public BaseController(IConnection Connection)
        {
            this.Connection = Connection;
        }

        #region " Direct Database Manipulation "
        public B Create()
        {
            return (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
        }
        public B Retrieve()
        {
            return (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
        }
        public B Update()
        {
            return (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
        }
        public B Delete()
        {
            return (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
        }
        public B Find()
        {
            return (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
        }
        public B Count()
        {
            return (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
        }
        public B Duplicate(M Source)
        {
            var result = (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
            if (Source != null)
            {
                result = (B)result.Clone(Source);
            }
            return result;
        }
        public B Retrieve(M Source)
        {
            var result = (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
            if (Source != null)
            {
                result = (B)result.Clone(Source);
            }
            return result;
        }
        public B Update(M Source)
        {
            var result = (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
            if (Source != null)
            {
                result = (B)result.Clone(Source);
            }
            return result;
        }
        public B Delete(M Source)
        {
            var result = (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
            if (Source != null)
            {
                result = (B)result.Clone(Source);
            }
            return result;
        }
        public B Find(M Source)
        {
            var result = (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
            if (Source != null)
            {
                result = (B)result.Clone(Source);
            }
            return result;
        }
        public B Count(M Source)
        {
            var result = (B)Activator.CreateInstance(typeof(B), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this, MethodBase.GetCurrentMethod().Name }, null);
            if (Source != null)
            {
                result = (B)result.Clone(Source);
            }
            return result;
        }
        #endregion

        #region " Editor Window ";
        private void ClearEditor()
        {
            if (this.editorModel != null)
            {
                this.editorModel = default(M);
            }
            if (this.editorResult != null)
            {
                this.editorResult = null;
            }
            if (this.editorWindow != null)
            {
                this.editorWindow = null;
            }
        }

        private void Editor_Saved(object sender, GenericEventArgs<bool> e)
        {
            B builder;
            if (editorModel == null || editorModel.ID == 0)
            {
                builder = this.Create();
            }
            else
            {
                builder = this.Update(this.editorModel);
            }

            var result = this.PrepareBuilder(builder).Execute();

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

        protected abstract M CreateModel();
        protected abstract IWindowEditor CreateEditor();
        protected abstract B PrepareBuilder(B builder);

        protected abstract string TitleCreate { get; }
        protected abstract string TitleView { get; }
        protected abstract string TitleEdit { get; }

        protected M editorModel { get; private set; }
        protected IWindowEditor editorWindow { get; private set; }
        protected IQueryResult<M> editorResult { get; private set; }

        public IQueryResult<M> ShowCreate(IWin32Window owner)
        {
            this.ClearEditor();
            this.editorModel = this.CreateModel();
            using (this.editorWindow = this.CreateEditor())
            {
                editorWindow.Text = TitleCreate;
                editorWindow.Mode = DynamicEditorModes.Edit;
                editorWindow.CanChangeMode = false;
                editorWindow.Saved += Editor_Saved;
                editorWindow.ShowDialog(owner);
            }
            return this.editorResult;
        }

        public void ShowViewer(IWin32Window owner, ulong ID)
        {
            this.ShowViewer(owner, (B)this.Retrieve().ID(ID));
        }
        public void ShowViewer(IWin32Window owner, B Source)
        {
            var result = Source.Execute();
            M model = default(M);
            if (result.Records != null && result.Records.Length > 0)
            {
                model = result.Records[0];
            }
            result = null;
            this.ShowViewer(owner, model);
        }
        public void ShowViewer(IWin32Window owner, M Source)
        {
            this.ClearEditor();
            this.editorModel = Source;
            using (this.editorWindow = this.CreateEditor())
            {
                editorWindow.Text = TitleView + Source.ID.ToString();
                editorWindow.Mode = DynamicEditorModes.View;
                editorWindow.CanChangeMode = false;
                editorWindow.ShowDialog(owner);
            }
        }

        public IQueryResult<M> ShowEditor(IWin32Window owner, ulong ID)
        {
            return this.ShowEditor(owner, (B)this.Retrieve().ID(ID));
        }
        public IQueryResult<M> ShowEditor(IWin32Window owner, B Source)
        {
            var result = Source.Execute();
            M model = default(M);
            if (result.Records != null && result.Records.Length > 0)
            {
                model = result.Records[0];
            }
            result = null;
            return this.ShowEditor(owner, model);
        }
        public IQueryResult<M> ShowEditor(IWin32Window owner, M Source)
        {
            this.ClearEditor();
            this.editorModel = Source;
            using (this.editorWindow = this.CreateEditor())
            {
                editorWindow.Text = TitleEdit + Source.ID.ToString();
                editorWindow.Mode = DynamicEditorModes.Edit;
                editorWindow.CanChangeMode = false;
                editorWindow.Saved += Editor_Saved;
                editorWindow.ShowDialog(owner);
            }
            return this.editorResult;
        }
        #endregion

        #region " Collection Window "
        private void ClearCollection()
        {
            if (this.collectionObject != null)
            {
                this.collectionObject = default(C);
            }
            if (this.collectionWindow != null)
            {
                this.collectionWindow = null;
            }
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
                this.ParseRow(item);
            }
        }

        private void CollectionWindow_NewRow(object sender, EventArgs e)
        {
            if (collectionWindow == null || collectionObject == null) { return; }
            try
            {
                if (this.ShowCreate(collectionWindow).Successful == true)
                {
                    this.collectionObject.Refresh();
                }
            }
            catch { }
        }

        protected abstract IWindowCollection CreateCollection();
        protected abstract void ParseRow(M item);

        protected C collectionObject { get; private set; }
        protected IWindowCollection collectionWindow { get; private set; }
        
        public void ShowCollection()
        {
            this.ShowCollection(this.Retrieve().GetCollection());
        }
        public void ShowCollection(B Source)
        {
            this.ShowCollection(Source.GetCollection());
        }
        public void ShowCollection(C Source)
        {
            this.collectionObject = Source;
            try
            {
                this.collectionWindow = this.CreateCollection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Collection Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            collectionObject.Refresh();

            this.collectionWindow.AllowImport = false;
            this.collectionWindow.AllowExport = false;
            this.collectionWindow.Columns = collectionObject.Columns;

            if (collectionObject.Records != null)
            {
                foreach (var item in collectionObject.Records)
                {
                    this.ParseRow(item);
                }
            }
            this.collectionWindow.NewRow += CollectionWindow_NewRow;
            this.collectionWindow.DeleteRow += CollectionWindow_DeleteRow;
            this.collectionWindow.ModifyRow += CollectionWindow_ModifyRow;
            this.collectionWindow.ViewRow += CollectionWindow_ViewRow;

            collectionObject.ListRefreshed += CollectionObject_ListRefreshed;

            ((IWindowCollection)this.collectionWindow).Show();
        }
        #endregion
    }
}
