using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden.Engines
{
    public sealed class MssqlEngine : IConnection
    {
        internal MssqlEngine(IConnectionBuilder builder)
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
            Console.WriteLine(ConnectionString);
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
            QueryResult<M> result = new QueryResult<M>();

            using (var connection = this.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = generateQuery<M>(query, default(M));
                    if (query.Parameters.Length > 0)
                    {
                        foreach (var item in query.Parameters)
                        {
                            //((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    try
                    {
                        EngineHelper.queryCount<M>(ref result, command);
                    }
                    catch (Exception ex)
                    {
                        result.s_ErrorMessage = ex.Message;
                    }
                }
            }

            return result;
        }

        public IQueryResult<M> List<M>(IQuery query) where M : IModel
        {
            QueryResult<M> result = new QueryResult<M>();

            using (var connection = this.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = generateQuery<M>(query, default(M));
                    if (query.Parameters.Length > 0)
                    {
                        foreach (var item in query.Parameters)
                        {
                            //((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    try
                    {
                        EngineHelper.querySelect<M>(ref result, query, command, this);
                    }
                    catch (Exception ex)
                    {
                        result.s_ErrorMessage = ex.Message;
                    }
                }
            }

            return result;
        }

        public IQueryResult<M> Retrieve<M>(IQuery query) where M : IModel
        {
            QueryResult<M> result = new QueryResult<M>();

            using (var connection = this.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = generateQuery<M>(query, default(M));
                    if (query.Parameters.Length > 0)
                    {
                        foreach (var item in query.Parameters)
                        {
                            //((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    try
                    {
                        EngineHelper.querySelect<M>(ref result, query, command, this);
                    }
                    catch (Exception ex)
                    {
                        result.s_ErrorMessage = ex.Message;
                    }
                }
            }

            return result;
        }

        public IQueryResult<M> Create<M>(M data) where M : IModel
        {
            IQuery query = new QueryBuilder<M>().Build();
            QueryResult<M> result = new QueryResult<M>();

            using (var connection = this.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = generateQuery<M>(query, data);
                    if (query.Parameters.Length > 0)
                    {
                        foreach (var item in query.Parameters)
                        {
                            //((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    try
                    {
                        EngineHelper.queryNonQuery<M>(ref result, command);
                        //result.i_InsertId = ((int)((MySqlCommand)command).LastInsertedId);
                    }
                    catch (Exception ex)
                    {
                        result.s_ErrorMessage = ex.Message;
                    }
                }
            }

            return result;
        }

        public IQueryResult<M> Update<M>(IQuery query, M data) where M : IModel
        {
            QueryResult<M> result = new QueryResult<M>();

            using (var connection = this.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = generateQuery<M>(query, data);
                    if (query.Parameters.Length > 0)
                    {
                        foreach (var item in query.Parameters)
                        {
                            //((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    try
                    {
                        EngineHelper.queryNonQuery<M>(ref result, command);
                    }
                    catch (Exception ex)
                    {
                        result.s_ErrorMessage = ex.Message;
                    }
                }
            }

            return result;
        }

        public IQueryResult<M> Delete<M>(IQuery query) where M : IModel
        {
            QueryResult<M> result = new QueryResult<M>();

            using (var connection = this.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = generateQuery<M>(query, default(M));
                    if (query.Parameters.Length > 0)
                    {
                        foreach (var item in query.Parameters)
                        {
                            //((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    try
                    {
                        EngineHelper.queryNonQuery<M>(ref result, command);
                    }
                    catch (Exception ex)
                    {
                        result.s_ErrorMessage = ex.Message;
                    }
                }
            }

            return result;
        }
        #endregion

        #region " Database Operations "
        public IDatabaseBackup Backup(params Type[] Models)
        {
            IDatabaseBackup backup = EngineHelper.createBackup(this, Models);
            // TODO: build table structures
            return backup;
        }

        public IDatabaseBackup Backup<T1>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }
        public IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>() { return this.Backup(MethodBase.GetCurrentMethod().GetGenericArguments()); }

        public IDatabaseRestore Restore(IDatabaseBackup Backup)
        {
            return EngineHelper.restoreFromBackup(this, Backup);
        }

        public IResult Compare(IDatabaseStructure Structure)
        {
            return EngineHelper.compareToStructure(this, Structure);
        }

        public IResult Install(IDatabaseStructure Structure)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}