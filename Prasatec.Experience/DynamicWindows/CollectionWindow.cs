using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience.DynamicWindows
{
    public partial class CollectionWindow : BaseWindows.BasePaddedWindow, IWindowCollection
    {
        protected IWindowCollection Controller { get { return (IWindowCollection)this; } }
        IWindow IWindowCollection.Base { get { return this; } }

        #region " Categories "
        public event EventHandler<CollectionCategoryChangedEventArgs> CategoryChanged;
        private IElementDynamicTableLayout Categories { get { return this.cCategories;  } }

        bool IWindowCollection.Categorized
        {
            get
            {
                return this.Categories.Count > 0;
            }
        }

        event EventHandler<CollectionCategoryChangedEventArgs> IWindowCollection.CategoryChanged
        {
            add
            {
                this.CategoryChanged += value;
            }

            remove
            {
                this.CategoryChanged -= value;
            }
        }

        void IWindowCollection.AddCategory(string Name, string Label, object[] Values)
        {
            Categories.AddDropdown(Name, Label, Values, -1);
            if (Categories.Visible != ((IWindowCollection)this).Categorized)
            {
                Categories.Visible = ((IWindowCollection)this).Categorized;
            }
        }

        int IWindowCollection.GetCategory(string Name)
        {
            return Categories.GetValue<int>(Name);
        }

        public void SetCategory(string Name, int Value)
        {
            Categories.SetValue(Name, Value);
        }

        void IWindowCollection.RemoveCategory(string Name)
        {
            Categories.RemoveControl(Name);
            if (Categories.Visible != ((IWindowCollection)this).Categorized)
            {
                Categories.Visible = ((IWindowCollection)this).Categorized;
            }
        }

        private void Categories_DropdownChanged(object sender, GenericEventArgs<string> e)
        {
             this.CategoryChanged?.Invoke(this, new CollectionCategoryChangedEventArgs(Categories.GetValue<int>(e.Value), e.Value));
        }
        #endregion

        #region " Pagination "
        public event EventHandler<GenericEventArgs<int>> PageChanged;
        private int i_PageCount, i_PageIndex;
        event EventHandler<GenericEventArgs<int>> IWindowCollection.PageChanged
        {
            add
            {
                this.PageChanged += value;
            }

            remove
            {
                this.PageChanged -= value;
            }
        }

        int IWindowCollection.PageCount
        {
            get
            {
                return this.i_PageCount;
            }

            set
            {
                if (value < 1) { value = 1; }
                this.i_PageCount = value;
                if (this.i_PageIndex > i_PageCount) { this.i_PageIndex = value; }
                if (this.i_PageIndex < 1) { this.i_PageIndex = 1; }
                this.PageChanged?.Invoke(this, new GenericEventArgs<int>(value));
                this.formatLabel();
            }
        }

        int IWindowCollection.PageIndex
        {
            get
            {
                return this.i_PageIndex;
            }

            set
            {
                if (value < 1) { value = 1; }
                if (value > this.i_PageCount) { value = this.i_PageCount; }
                this.i_PageIndex = value;
                this.PageChanged?.Invoke(this, new GenericEventArgs<int>(value));
                this.formatLabel();
            }
        }

        bool IWindowCollection.Paginated
        {
            get
            {
                return this.i_PageCount > 1;
            }
        }

        private void cPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((IWindowCollection)this).PageIndex = cPage.SelectedIndex + 1;
        }

        private void bFirstPage_Click(object sender, EventArgs e)
        {
            ((IWindowCollection)this).PageIndex = 1;
        }

        private void bPreviousPage_Click(object sender, EventArgs e)
        {
            ((IWindowCollection)this).PageIndex--;
        }

        private void bNextPage_Click(object sender, EventArgs e)
        {
            ((IWindowCollection)this).PageIndex++;
        }

        private void bLastPage_Click(object sender, EventArgs e)
        {
            ((IWindowCollection)this).PageIndex = ((IWindowCollection)this).PageCount;
        }
        private void formatLabel()
        {
            if (this.i_PageCount == 1)
            {
                pPaginate.Visible = false;
            }
            else
            {
                if (!pPaginate.Visible) { pPaginate.Visible = true; }
            }
            if (i_PageIndex == 1)
            {
                bPreviousPage.Enabled = false;
                bFirstPage.Enabled = false;
                if (!bNextPage.Enabled) { bNextPage.Enabled = true; }
                if (!bLastPage.Enabled) { bLastPage.Enabled = true; }
            }
            else if (i_PageIndex == i_PageCount)
            {
                bNextPage.Enabled = false;
                bLastPage.Enabled = false;
                if (!bPreviousPage.Enabled) { bPreviousPage.Enabled = true; }
                if (!bFirstPage.Enabled) { bFirstPage.Enabled = true; }
            }
            else
            {
                if (!bPreviousPage.Enabled) { bPreviousPage.Enabled = true; }
                if (!bFirstPage.Enabled) { bFirstPage.Enabled = true; }
                if (!bNextPage.Enabled) { bNextPage.Enabled = true; }
                if (!bLastPage.Enabled) { bLastPage.Enabled = true; }
            }
            lPage.Text = string.Format("Page {0} of {1}", i_PageIndex, i_PageCount);
            if (cPage.Items.Count != i_PageCount)
            {
                cPage.Items.Clear();
                cPage.Items.AddRange(Enumerable.Range(1, i_PageCount).Cast<object>().ToArray());
            }
            if (cPage.SelectedIndex != i_PageIndex - 1)
            {
                cPage.SelectedIndex = i_PageIndex - 1;
            }
        }
        #endregion

        #region " Form Buttons "
        public event EventHandler PerformExport, PerformImport;
        event EventHandler IWindowCollection.PerformExport
        {
            add
            {
                this.PerformExport += value;
            }

            remove
            {
                this.PerformExport -= value;
            }
        }

        event EventHandler IWindowCollection.PerformImport
        {
            add
            {
                this.PerformImport += value;
            }

            remove
            {
                this.PerformExport -= value;
            }
        }

        bool IWindowCollection.AllowExport
        {
            get
            {
                return bExport.Visible;
            }

            set
            {
                bExport.Enabled = value;
                bExport.Visible = value;
                reorderFormButtons();
            }
        }

        bool IWindowCollection.AllowImport
        {
            get
            {
                return bImport.Visible;
            }

            set
            {
                bImport.Enabled = value;
                bImport.Visible = value;
                reorderFormButtons();
            }
        }
        private void reorderFormButtons()
        {
            bImport.BringToFront();
            bExport.BringToFront();
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            this.PerformImport?.Invoke(this, e);
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            this.PerformExport?.Invoke(this, e);
        }
        #endregion

        #region " Listview Actions "
        event EventHandler<GenericArrayEventArgs<int>> DeleteRow;
        event EventHandler<GenericEventArgs<int>> ModifyRow, ViewRow;
        event EventHandler NewRow;
        event EventHandler<GenericArrayEventArgs<int>> IWindowCollection.DeleteRow
        {
            add
            {
                this.DeleteRow += value;
            }

            remove
            {
                this.DeleteRow -= value;
            }
        }

        event EventHandler<GenericEventArgs<int>> IWindowCollection.ModifyRow
        {
            add
            {
                this.ModifyRow += value;
            }

            remove
            {
                this.ModifyRow -= value;
            }
        }

        event EventHandler<GenericEventArgs<int>> IWindowCollection.ViewRow
        {
            add
            {
                this.ViewRow += value;
            }

            remove
            {
                this.ViewRow -= value;
            }
        }

        event EventHandler IWindowCollection.NewRow
        {
            add
            {
                this.NewRow += value;
            }

            remove
            {
                this.NewRow -= value;
            }
        }

        bool IWindowCollection.AllowNew
        {
            get
            {
                return pNew.Visible;
            }

            set
            {
                pNew.Enabled = value;
                pNew.Visible = value;
                reorderActionButtons();
            }
        }

        bool IWindowCollection.AllowDelete
        {
            get
            {
                return bDelete.Visible;
            }

            set
            {
                bDelete.Enabled = value == false ? false : cList.SelectedIndices.Count > 0;
                bDelete.Visible = value;
                reorderActionButtons();
            }
        }

        bool IWindowCollection.AllowModify
        {
            get
            {
                return bModify.Visible;
            }

            set
            {
                bModify.Enabled = value == false ? false : cList.SelectedIndices.Count == 1;
                bModify.Visible = value;
                reorderActionButtons();
            }
        }

        bool IWindowCollection.AllowView
        {
            get
            {
                return bView.Visible;
            }

            set
            {
                bView.Enabled = value == false ? false : cList.SelectedIndices.Count == 1;
                bView.Visible = value;
                reorderActionButtons();
            }
        }
        string[] IWindowCollection.Columns
        {
            get
            {
                return cList.Columns.OfType<ColumnHeader>().Select(s => s.Text).ToArray();
            }

            set
            {
                cList.Columns.Clear();
                foreach (string item in value)
                {
                    cList.Columns.Add(item, 100);
                }
            }
        }

        int[] IWindowCollection.SelectedRows
        {
            get
            {
                return cList.SelectedIndices.OfType<int>().ToArray();
            }

            set
            {
                cList.SelectedIndices.Clear();
                foreach (var item in value)
                {
                    cList.SelectedIndices.Add(item);
                }
            }
        }

        void IWindowCollection.AddRow(params object[] columns)
        {
            if (columns == null || columns.Length == 0) { return; }
            ListViewItem item = new ListViewItem(columns[0]?.ToString());
            for (int i = 1; i < columns.Length; i++)
            {
                item.SubItems.Add(columns[i]?.ToString());
            }
            cList.Items.Add(item);

            for (int i = 0; i < columns.Length; i++)
            {
                cList.Columns[i].Width = -1;
                if (cList.Columns[i].Width < 100)
                {
                    cList.Columns[i].Width = 100;
                }
            }
        }

        void IWindowCollection.ClearRows()
        {
            cList.SelectedIndices.Clear();
            cList.Items.Clear();
        }

        void IWindowCollection.RemoveRow(int Index)
        {
            cList.Items.RemoveAt(Index);
        }

        private void reorderActionButtons()
        {
            bDelete.BringToFront();
            bModify.BringToFront();
            bView.BringToFront();
            if (!bDelete.Visible && !bModify.Visible && !bView.Visible && !pNew.Visible && pActions.Visible)
            {
                pActions.Visible = false;
            }
            else if (bDelete.Visible || bModify.Visible || bView.Visible || pNew.Visible || !pActions.Visible)
            {
                pActions.Visible = true;
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (cList.SelectedIndices.Count > 0)
            {
                this.DeleteRow?.Invoke(this, new GenericArrayEventArgs<int>(Controller.SelectedRows));
            }
        }

        private void bModify_Click(object sender, EventArgs e)
        {
            if (cList.SelectedIndices.Count == 1)
            {
                this.ModifyRow?.Invoke(this, new GenericEventArgs<int>(cList.SelectedIndices[0]));
            }
        }

        private void bView_Click(object sender, EventArgs e)
        {
            if (cList.SelectedIndices.Count == 1)
            {
                this.ViewRow?.Invoke(this, new GenericEventArgs<int>(cList.SelectedIndices[0]));
            }
        }

        private void cList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cList.SelectedIndices.Count == 0)
            {
                bView.Enabled = false;
                bDelete.Enabled = false;
                bModify.Enabled = false;
            }
            else if (cList.SelectedIndices.Count == 1)
            {
                bView.Enabled = true;
                bDelete.Enabled = true;
                bModify.Enabled = true;
            }
            else if (cList.SelectedIndices.Count > 1)
            {
                bView.Enabled = false;
                bDelete.Enabled = true;
                bModify.Enabled = false;
            }
        }

        private void bNew_Click(object sender, EventArgs e)
        {
            this.NewRow?.Invoke(this, e);
        }
        #endregion

        public CollectionWindow()
        {
            InitializeComponent();

            Categories.DropdownChanged += Categories_DropdownChanged;
            Categories.Mode = DynamicEditorModes.Edit;
            ((IWindowCollection)this).PageCount = 1;
            cList_SelectedIndexChanged(null, null);
            cList.FullRowSelect = true;
            cList.HideSelection = false;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}