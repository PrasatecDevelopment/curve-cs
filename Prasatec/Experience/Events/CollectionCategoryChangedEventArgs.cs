using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience
{
    public class CollectionCategoryChangedEventArgs : EventArgs
    {
        public Int32 Index { get; private set; }
        public String Name { get; private set; }
        public CollectionCategoryChangedEventArgs(Int32 Index, String Name)
        {
            this.Index = Index;
            this.Name = Name;
        }
    }
}
