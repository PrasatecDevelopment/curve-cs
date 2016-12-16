using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface IQueryResult<M> : IResult
        where M : IModel
    {
        Object ScalarValue { get; }
        Int32 AffectedRows { get; }
        Int32 InsertId { get; }
        Int32 CountRecords { get; }
        Int32 CountTotal { get; }
        Int32 CountPages { get; }
        M[] Records { get; }
    }
}
