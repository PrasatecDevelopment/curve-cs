using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience
{
    public class CollectionPageChangedEventArgs : EventArgs
    {
        public Int32 Page { get; private set; }
        public CollectionPageChangedEventArgs(Int32 Page)
        {
            this.Page = Page;
        }
    }
}
