using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com
{
    public sealed class DatabaseRestore : IDatabaseRestore
    {
        internal DatabaseRestore()
        {

        }
#pragma warning disable CS0649
        internal bool b_Successful;
#pragma warning restore CS0649

        public bool Successful
        {
            get
            {
                return b_Successful;
            }
        }
    }
}
