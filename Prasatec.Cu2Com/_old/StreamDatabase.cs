using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com
{
    internal enum QueryToPacketCodes : byte
    {
        TableList = 20,
        ColumnList = 21,
        ConditionList = 22,
        Table = 50,
        Column = 51,
        Condition = 52

    }

    /// <summary>
    /// Parses the database query and sends across the network stream
    /// </summary>
    public sealed class StreamDatabase : IConnection
    {
        internal StreamDatabase(IConnectionBuilder builder)
        {
            this.CreateConnectionString(builder);
            this.ActiveUserId = 0;
            this.def = Plink.Definitions.Retrieve();
        }

        private Prasatec.Plink.Definitions def;

        public uint ActiveUserId { get; set; }
        public String Database { get; private set; }

        #region " Connection "
        private string ConnectionString;
        private void CreateConnectionString(IConnectionBuilder builder)
        {
            this.ConnectionString = null;
            throw new NotImplementedException();
        }
        private IDbConnection CreateConnection()
        {
            Console.WriteLine(this.ConnectionString);
            throw new NotImplementedException();
        }
        #endregion

        #region " Code Parsing "
        private string parseTable(string[] tables)
        {
            throw new NotImplementedException();
        }

        private string parseColumn(DatabaseColumn[] columns)
        {
            throw new NotImplementedException();
        }

        private string parseColumn(QueryColumnActions Action, string Table, string Name)
        {
            throw new NotImplementedException();
        }

        private string parseCondition(DatabaseCondition[] conditions)
        {
            throw new NotImplementedException();
        }

        private string parseGroup(string[] columns)
        {
            throw new NotImplementedException();
        }

        private string parseSort(DatabaseSort[] sorts)
        {
            throw new NotImplementedException();
        }

        private string parseLimit(int startingIndex, int recordLimit)
        {
            throw new NotImplementedException();
        }
        private string generateQuery<M>(IQuery query, M data)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region " General Commands "
        public IQueryResult<M> Count<M>(IQuery query) where M : IModel
        {
            throw new NotImplementedException();
        }

        public IQueryResult<M> List<M>(IQuery query) where M : IModel
        {
            throw new NotImplementedException();
        }

        public IQueryResult<M> Retrieve<M>(IQuery query) where M : IModel
        {
            throw new NotImplementedException();
        }

        public IQueryResult<M> Create<M>(M data) where M : IModel
        {
            throw new NotImplementedException();
        }

        public IQueryResult<M> Update<M>(IQuery query, M data) where M : IModel
        {
            throw new NotImplementedException();
        }

        public IQueryResult<M> Delete<M>(IQuery query) where M : IModel
        {
            throw new NotImplementedException();
        }
        #endregion

        #region " Database Operations "
        public IDatabaseBackup Backup(params Type[] Models)
        {
            throw new NotImplementedException();
        }

        public IDatabaseRestore Restore(IDatabaseBackup Backup)
        {
            throw new NotImplementedException();
        }

        public IResult Compare(IDatabaseStructure Structure)
        {
            throw new NotImplementedException();
        }

        public IResult Install(IDatabaseStructure Structure)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}