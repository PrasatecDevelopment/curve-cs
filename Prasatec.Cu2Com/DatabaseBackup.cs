using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com
{
    public sealed class DatabaseBackup : IDatabaseBackup
    {
        internal DatabaseBackup()
        {

        }
#pragma warning disable CS0649
        internal IDatabaseStructure o_Structure;
        internal bool b_Successful;
#pragma warning restore CS0649
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
