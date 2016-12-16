using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface IDatabaseBackup
    {
        Boolean Successful { get; }
        IDatabaseStructure Structure { get; }
    }
}
