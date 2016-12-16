using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface IQueryColumn
    {
        String Identifier { get; }
        String Alias { get; }
        QueryColumnActions Action { get; }
    }
}
