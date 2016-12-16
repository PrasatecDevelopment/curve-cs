using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface ICollection<M>
    {
        event EventHandler ListRefreshed;

        Int32 PageIndex { get; set; }
        Int32 PageCount { get; }
        Int32 RecordsPerPage { get; set; }
        
        M[] Records { get; }
        
        bool Next();
        bool Previous();
        void FirstPage();
        void LastPage();
        void Refresh();
        void Reload();
        
        String[] Columns { get; }
    }
}
