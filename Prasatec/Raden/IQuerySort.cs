using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface IQuerySort
    {
        String Identifier { get; }
        QuerySortDirections Direction { get; }
    }
}
