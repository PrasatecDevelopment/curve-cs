using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal sealed class QueryResult<M> : IQueryResult<M>
        where M : IModel
    {
        internal QueryResult() { }

        internal int i_CountPages, i_CountRecords, i_CountTotal, i_AffectedRows, i_InsertId;
        internal string s_ErrorMessage;
        internal M[] o_Records;
        internal object o_ScalarValue;
        internal bool b_Successful;

        public int AffectedRows { get { return this.i_AffectedRows; } }
        public int InsertId { get { return this.i_InsertId; } }
        public int CountPages { get { return this.i_CountPages; } }
        public int CountRecords { get { return this.i_CountRecords; } }
        public int CountTotal { get { return this.i_CountTotal; } }
        public string ErrorMessage { get { return this.s_ErrorMessage; } }
        public M[] Records { get { return this.o_Records; } }
        public object ScalarValue { get { return this.o_ScalarValue; } }
        public bool Successful { get { return this.b_Successful; } }
    }
}
