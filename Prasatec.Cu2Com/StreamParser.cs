using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com
{
    /// <summary>
    /// Converts the database commands from the stream back into IQuery
    /// </summary>
    class StreamParser : IQuery
    {
        public DatabaseColumn[] Columns
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DatabaseCondition[] Conditions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string[] GroupBy
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public KeyValuePair<string, object>[] Parameters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int RecordLimit
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DatabaseSort[] SortBy
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int StartingIndex
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string[] Tables
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
