using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden.Engines
{
    public sealed class MysqlEngine : IConnection
    {
        internal MysqlEngine(IConnectionBuilder builder)
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
            string _server = builder.Server()?.Trim();
            string _username = builder.Username()?.Trim();
            string _database = builder.Database()?.Trim();
            string _password = builder.Password()?.Trim();
            int _port = builder.Port();
            EncryptionOptions _encrypted = builder.Encryption();

            if (_server == null || _server.Length == 0) { throw new MysqlConnectionFailedException(MysqlConnectionFailedReasons.NoServer); }
            if (_username == null || _username.Length == 0) { throw new MysqlConnectionFailedException(MysqlConnectionFailedReasons.NoUsername); }
            if (_database == null || _database.Length == 0) { throw new MysqlConnectionFailedException(MysqlConnectionFailedReasons.NoDatabase); }

            StringBuilder connectionStringBuilder = new StringBuilder();

            connectionStringBuilder.AppendFormat("Server={0};", _server);
            if (_port != 3306 && _port != 0)
            {
                connectionStringBuilder.AppendFormat("Port={0};", _port);
            }
            connectionStringBuilder.AppendFormat("Uid={0};", _database);
            connectionStringBuilder.AppendFormat("Database={0};", _username);
            if (_password != null && _password.Length > 0)
            {
                connectionStringBuilder.AppendFormat("Pwd={0};", _password);
            }
            if (_encrypted != EncryptionOptions.None)
            {
                connectionStringBuilder.AppendFormat("SslMode={0};", _encrypted == EncryptionOptions.SSLPreferred ? "Preferred" : "Required");
            }

            string cs = connectionStringBuilder.ToString();
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection())
            {
                connection.ConnectionString = cs;
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Access denied for user"))
                    {
                        throw new MysqlConnectionFailedException(MysqlConnectionFailedReasons.LoginFailed);
                    }
                    else if (ex.Message.Contains("Unable to connect to any of the specified MySQL hosts"))
                    {
                        throw new MysqlConnectionFailedException(MysqlConnectionFailedReasons.ConnectionFailed);
                    }
                    else if (ex.Message.Contains("does not support SSL connections"))
                    {
                        throw new MysqlConnectionFailedException(MysqlConnectionFailedReasons.EncryptionFailed);
                    }
                    else
                    {
                        throw new MysqlConnectionFailedException(MysqlConnectionFailedReasons.NotSure);
                    }
                }
            }
            this.ConnectionString = cs;
            this.Database = _database;
        }
        private IDbConnection CreateConnection()
        {
            var result = new MySqlConnection();
            result.ConnectionString = this.ConnectionString;
            return result;
        }
        #endregion

        #region " Code Parsing "
        private string parseTable(string[] tables)
        {
            string result = "";
            foreach (string table in tables)
            {
                if (result.Length != 0) { result = result + ", "; }
                result = result + "`" + table + "`";
            }
            return result;
        }

        private string parseColumn(DatabaseColumn[] columns)
        {
            string result = "";
            if (columns.Length > 0)
            {
                foreach (DatabaseColumn column in columns)
                {
                    if (result.Length != 0) { result = result + ", "; }
                    result = result + parseColumn(column.Action, column.Table, column.Name);
                    if (column.Alias != null)
                    {
                        result = result + " AS `" + column.Alias + "`.";
                    }
                }
            }
            else { return "*"; }
            return result;
        }

        private string parseColumn(QueryColumnActions Action, string Table, string Name)
        {
            string result = "";
            if (Action != QueryColumnActions.None)
            {
                switch (Action)
                {
                    case QueryColumnActions.Count:
                        result = result + "COUNT";
                        break;
                }
                result = result + "(";
            }
            if (Table != null)
            {
                result = result + "`" + Table + "`.";
            }
            result = result + "`" + Name + "`";
            if (Action != QueryColumnActions.None)
            {
                result = result + ")";
            }
            return result;
        }

        private string parseCondition(DatabaseCondition[] conditions)
        {
            string result = "";
            int depth = 0;
            foreach (DatabaseCondition condition in conditions)
            {
                if (depth > condition.Depth)
                {
                    result = result + (new String(Convert.ToChar(")"), depth - condition.Depth));
                    depth = condition.Depth;
                }
                if (result.Length > 0)
                {
                    result = result + (condition.LogicalOperator == QueryLogicalOperators.LogicalAnd ? " AND " : " OR ");
                }
                if (depth < condition.Depth)
                {
                    result = result + (new String(Convert.ToChar("("), condition.Depth - depth));
                    depth = condition.Depth;
                }
                result = result + "(";
                result = result + this.parseColumn(condition.Action, condition.Table1, condition.Column1);
                if (condition.ComparisonOperator == QueryComparisonOperators.IsNull)
                {
                    result = result + (" IS NULL");
                }
                else if (condition.ComparisonOperator == QueryComparisonOperators.IsNotNull)
                {
                    result = result + (" IS NOT NULL");
                }
                else
                {
                    result = result + " " + EngineHelper.getOperator(condition.ComparisonOperator) + " ";
                    if (condition.Column2 != null)
                    {
                        result = result + this.parseColumn(QueryColumnActions.None, condition.Table2, condition.Column2);
                    }
                    else
                    {
                        result = result + condition.Value;
                    }
                }
                result = result + ")";
            }
            if (depth > 0)
            {
                result = result + (new String(Convert.ToChar(")"), depth));
                depth = 0;
            }
            return result;
        }

        private string parseGroup(string[] columns)
        {
            string result = "";
            foreach (string column in columns)
            {
                if (result.Length != 0) { result = result + ", "; }
                result = result + "`" + column + "`";
            }
            return result;
        }

        private string parseSort(DatabaseSort[] sorts)
        {
            string result = "";
            if (sorts.Length > 0)
            {
                foreach (DatabaseSort sort in sorts)
                {
                    if (result.Length != 0) { result = result + ", "; }
                    if (sort.Table != null)
                    {
                        result = result + "`" + sort.Table + "`.";
                    }
                    result = result + "`" + sort.Column + "`";
                    if (sort.Direction != QuerySortDirections.Default)
                    {
                        result = result + " " + (sort.Direction == QuerySortDirections.Ascending ? "ASC" : "DESC");
                    }
                }
            }
            return result;
        }

        private string parseLimit(int startingIndex, int recordLimit)
        {
            string result = "";
            if (recordLimit > 0)
            {
                if (startingIndex > 0)
                {
                    result = result + startingIndex.ToString() + ", ";
                }
                result = result + recordLimit.ToString();
            }
            return result;
        }
        private string generateQuery<M>(IQuery query, M data)
        {
            StringBuilder sql = new StringBuilder();
            StackTrace trace = new StackTrace();
            string caller = trace.GetFrame(1).GetMethod().Name.ToLower();

            if (data != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                object value = null;
                bool skip = false;

                foreach (KeyValuePair<string, object> item in query.Parameters)
                {
                    parameters.Add(item.Key, item.Value);
                }

                foreach (var property in def.GetProperties<M>().Where(x => x.RadenColumnName != null && x.RadenValueType != null))
                {
                    if (property.RadenAutoValue != null)
                    {
                        switch (property.RadenAutoValue)
                        {
                            case ColumnAutoValues.CreatedStamp:
                                if (caller == "create")
                                {
                                    value = new Timestamp(DateTime.Now);
                                }
                                else { value = property.GetValue(data); }
                                break;
                            case ColumnAutoValues.CreatedUser:
                                if (caller == "create")
                                {
                                    value = ActiveUserId;
                                }
                                else { value = property.GetValue(data); }
                                break;
                            case ColumnAutoValues.ModifiedStamp:
                                if (caller == "update")
                                {
                                    value = new Timestamp(DateTime.Now);
                                }
                                else { value = property.GetValue(data); }
                                break;
                            case ColumnAutoValues.ModifiedUser:
                                if (caller == "update")
                                {
                                    value = ActiveUserId;
                                }
                                else { value = property.GetValue(data); }
                                break;
                            case ColumnAutoValues.AutoIncrement:
                                skip = true;
                                break;
                            default:
                                value = property.GetValue(data);
                                break;
                        }
                    }
                    else { value = property.GetValue(data); }
                    if (skip == false)
                    {
                        if (value is ITimestamp)
                        {
                            value = ((ITimestamp)value).ToDouble();
                        }
                        else if (value is IForeignKey)
                        {
                            value = ((IForeignKey)value).getValue();
                        }
                        parameters.Add("@" + property.RadenColumnName, value);
                    }
                    skip = false;
                    value = null;
                }

                ((Query)query).o_Parameters = parameters.ToArray();
                parameters.Clear(); parameters = null;
            }

            if (caller == "delete")
            {
                sql.AppendFormat("DELETE FROM {0}", this.parseTable(query.Tables));

                if (query.Conditions.Where(x => !x.CanPerformAction).Count() > 0)
                {
                    sql.AppendFormat("\n    WHERE {0}", this.parseCondition(query.Conditions.Where(x => !x.CanPerformAction).ToArray()));
                }
                if (query.SortBy.Length > 0)
                {
                    sql.AppendFormat("\n    ORDER BY {0}", this.parseSort(query.SortBy));
                }
                if (query.RecordLimit > 0 || query.StartingIndex > 0)
                {
                    sql.AppendFormat("\n    LIMIT {0}", this.parseLimit(query.StartingIndex, query.RecordLimit));
                }
            }
            else if (caller == "create")
            {
                sql.AppendFormat("INSERT INTO {0}", this.parseTable(query.Tables));
                sql.AppendFormat("\n    (`{0}`)", String.Join("`, `", query.Parameters.Where(x => !x.Key.StartsWith("@PARAMETER")).Select(x => x.Key.Substring(1))));
                sql.Append("\n    VALUES");
                sql.AppendFormat("\n    ({0})", String.Join(", ", query.Parameters.Where(x => !x.Key.StartsWith("@PARAMETER")).Select(x => x.Key)));
            }
            else if (caller == "update")
            {
                sql.AppendFormat("UPDATE {0} SET ", this.parseTable(query.Tables));
                bool updateComma = false;
                foreach (var parameter in query.Parameters.Where(x => !x.Key.StartsWith("@PARAMETER")).Select(x => x.Key.Substring(1)))
                {
                    if (updateComma) { sql.Append(", "); } else { updateComma = true; }
                    sql.AppendFormat("`{0}` = @{0}", parameter);
                }

                if (query.Conditions.Where(x => !x.CanPerformAction).Count() > 0)
                {
                    sql.AppendFormat("\n    WHERE {0}", this.parseCondition(query.Conditions.Where(x => !x.CanPerformAction).ToArray()));
                }
                if (query.SortBy.Length > 0)
                {
                    sql.AppendFormat("\n    ORDER BY {0}", this.parseSort(query.SortBy));
                }
                if (query.RecordLimit > 0 || query.StartingIndex > 0)
                {
                    sql.AppendFormat("\n    LIMIT {0}", this.parseLimit(query.StartingIndex, query.RecordLimit));
                }
            }
            else
            {
                sql.AppendFormat("SELECT");
                if (caller == "count")
                {
                    sql.AppendFormat("\n    COUNT(`{0}`)", def.GetModule<M>().RadenTableIdColumn);
                }
                else
                {
                    sql.AppendFormat("\n    {0}", this.parseColumn(query.Columns));
                }

                sql.AppendFormat("\n    FROM {0}", this.parseTable(query.Tables));

                if (query.Conditions.Where(x => !x.CanPerformAction).Count() > 0)
                {
                    sql.AppendFormat("\n    WHERE {0}", this.parseCondition(query.Conditions.Where(x => !x.CanPerformAction).ToArray()));
                }
                if (query.GroupBy.Length > 0)
                {
                    sql.AppendFormat("\n    GROUP BY {0}", this.parseGroup(query.GroupBy));
                }
                if (query.Conditions.Where(x => x.CanPerformAction).Count() > 0)
                {
                    sql.AppendFormat("\n    HAVING {0}", this.parseCondition(query.Conditions.Where(x => x.CanPerformAction).ToArray()));
                }
                if (query.SortBy.Length > 0)
                {
                    sql.AppendFormat("\n    ORDER BY {0}", this.parseSort(query.SortBy));
                }
                if (caller == "retrieve")
                {
                    sql.Append("\n    LIMIT 1");
                }
                else
                {
                    if (query.RecordLimit > 0 || query.StartingIndex > 0)
                    {
                        sql.AppendFormat("\n    LIMIT {0}", this.parseLimit(query.StartingIndex, query.RecordLimit));
                    }
                }
            }
            return sql.ToString();
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
                            ((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
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
                            ((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    EngineHelper.querySelect<M>(ref result, query, command, this);
                    try
                    {
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
                            ((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
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
                            ((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    try
                    {
                        EngineHelper.queryNonQuery<M>(ref result, command);
                        result.i_InsertId = ((int)((MySqlCommand)command).LastInsertedId);
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
                            ((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
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
                            ((MySqlCommand)command).Parameters.AddWithValue(item.Key, item.Value);
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
            foreach (var model in Models)
            {
                // TODO: Create table code
            }
            return backup;
        }

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