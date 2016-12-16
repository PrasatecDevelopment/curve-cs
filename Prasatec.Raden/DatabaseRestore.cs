using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public sealed class DatabaseRestore : IDatabaseRestore
    {
        internal DatabaseRestore()
        {

        }
        internal bool b_Successful;

        public bool Successful
        {
            get
            {
                return b_Successful;
            }
        }
    }
}
