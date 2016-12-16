using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec
{
    public interface IResult
    {
        Boolean Successful { get; }
        String ErrorMessage { get; }
    }
}
