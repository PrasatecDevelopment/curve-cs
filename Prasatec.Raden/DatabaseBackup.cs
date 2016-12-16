using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public sealed class DatabaseBackup : IDatabaseBackup
    {
        internal DatabaseBackup()
        {

        }

        internal IDatabaseStructure o_Structure;
        internal bool b_Successful;

        public IDatabaseStructure Structure
        {
            get
            {
                return o_Structure;
            }
        }

        public bool Successful
        {
            get
            {
                return b_Successful;
            }
        }
    }
}
