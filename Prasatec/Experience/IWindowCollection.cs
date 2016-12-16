using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience
{
    public interface IWindowCollection : IWindow, IWin32Window
    {
        event EventHandler<CollectionCategoryChangedEventArgs> CategoryChanged;
        event EventHandler<GenericEventArgs<int>> PageChanged, ViewRow, ModifyRow;
        event EventHandler<GenericArrayEventArgs<int>> DeleteRow;
        event EventHandler NewRow, PerformImport, PerformExport;

        new IWindow Base { get; }

        Boolean Paginated { get; }
        Int32 PageIndex { get; set; }
        Int32 PageCount { get; set; }
        String[] Columns { get; set; }
        Int32[] SelectedRows { get; set; }
        Boolean Categorized { get; }
        Boolean AllowView { get; set; }
        Boolean AllowModify { get; set; }
        Boolean AllowDelete { get; set; }
        Boolean AllowNew { get; set; }
        Boolean AllowImport { get; set; }
        Boolean AllowExport { get; set; }

        void ClearRows();
        void AddRow(params object[] columns);
        void RemoveRow(Int32 Index); 
        void AddCategory(string Name, string Label, object[] Values);
        void RemoveCategory(string Name);
        Int32 GetCategory(string Name);
    }
}
