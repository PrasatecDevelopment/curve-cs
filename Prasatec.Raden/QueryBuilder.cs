using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public class QueryBuilder<M> : IQueryBuilder<M>
        where M : IModel
    {
        private List<IQueryTable> _tables;
        private List<IQueryColumn> _columns;
        private List<IQueryCondition> _conditions;
        private List<IQueryGroup> _grouping;
        private List<IQuerySort> _sorting;
        private IQueryLimit _limit;

        private Prasatec.Plink.Definitions def;

        public IQuery Build()
        {
            var result = new Query();
            List<KeyValuePair<string, object>> lParameters = new List<KeyValuePair<string, object>>();
            List<string> lTables = new List<string>();
            List<DatabaseColumn> lColumns = new List<DatabaseColumn>();
            List<DatabaseCondition> lConditions = new List<DatabaseCondition>();
            List<string> lGroupBy = new List<string>();
            List<DatabaseSort> lSortBy = new List<DatabaseSort>();

            if (_limit.RecordLimit < 0)
            {
                ((QueryLimit)_limit).i_RecordLimit = 0;
            }
            if (_limit.StartingIndex < 0)
            {
                ((QueryLimit)_limit).i_StartingIndex = 0;
            }

            result.i_RecordLimit = _limit.RecordLimit;
            result.i_StartingRecord = _limit.StartingIndex;

            foreach (var table in this.Tables)
            {
                lTables.Add(def.GetModule(table.Identifier).RadenTableName);
            }

            foreach (var column in this.Columns)
            {
                lColumns.Add(new Raden.DatabaseColumn(
                    this._tables.Count > 1 ? def.GetModule(def.GetProperty(column.Identifier).Parent).RadenTableName : null,
                    def.GetProperty(column.Identifier).Reference.Name == "ID" ? def.GetModule(def.GetProperty(column.Identifier).Parent).RadenTableIdColumn : def.GetProperty(column.Identifier).RadenColumnName,
                    column.Alias,
                    column.Action
                    ));
            }

            foreach (var condition in this.Conditions)
            {
                //var test = def.GetModule(condition.Identifier.Substring(0, condition.))
                //string t2;
                //var prop = ;
                //var t1 = ;
                //t2 = def.GetProperty(condition.Identifier).Reference.Name == "ID" ? def.GetModule(def.GetProperty(condition.Identifier).Parent).RadenTableIdColumn : def.GetProperty(condition.Identifier).RadenColumnName;
                DatabaseCondition newCondition = new DatabaseCondition(
                    this._tables.Count > 1 ? def.GetModule(def.GetProperty(condition.Identifier).Parent).RadenTableName : null,
                    def.GetProperty(condition.Identifier).Reference.Name == "ID" ? def.GetModule(def.GetProperty(condition.Identifier).Parent).RadenTableIdColumn : def.GetProperty(condition.Identifier).RadenColumnName,
                    condition.Logical, condition.Operator, condition.Depth
                    );
                if (condition is IQueryConditionByColumn)
                {
                    newCondition.SetColumn(
                        def.GetModule(def.GetProperty(((IQueryConditionByColumn)condition).ValueColumn).Parent).RadenTableName,
                        def.GetProperty(((IQueryConditionByColumn)condition).ValueColumn).Reference.Name == "ID" ? def.GetModule(def.GetProperty(((IQueryConditionByColumn)condition).ValueColumn).Parent).RadenTableIdColumn : def.GetProperty(((IQueryConditionByColumn)condition).ValueColumn).RadenColumnName
                    );
                }
                if (condition is IQueryConditionByValue)
                {
                    lParameters.Add(new KeyValuePair<string, object>("@PARAMETER" + (lParameters.Count + 1), ((IQueryConditionByValue)condition).Value));
                    newCondition.SetValue("@PARAMETER" + (lParameters.Count), ((IQueryConditionByValue)condition).Wildcard);
                }
                if (condition is IQueryConditionActionable)
                {
                    newCondition.SetAction(((IQueryConditionActionable)condition).Action);
                }
                lConditions.Add(newCondition);
            }

            foreach (var sort in this.Sorts)
            {
                lSortBy.Add(new DatabaseSort(
                    this._tables.Count > 1 ? def.GetModule(def.GetProperty(sort.Identifier).Parent).RadenTableName : null,
                    def.GetProperty(sort.Identifier).Reference.Name == "ID" ? def.GetModule(def.GetProperty(sort.Identifier).Parent).RadenTableIdColumn : def.GetProperty(sort.Identifier).RadenColumnName,
                    sort.Direction
                    ));
            }

            foreach (var group in this.Groups)
            {
                lGroupBy.Add(def.GetProperty(group.Identifier).Reference.Name == "ID" ? def.GetModule(def.GetProperty(group.Identifier).Parent).RadenTableIdColumn : def.GetProperty(group.Identifier).RadenColumnName);
            }
            result.o_Parameters = lParameters.ToArray();
            result.o_Tables = lTables.ToArray();
            result.o_Columns = lColumns.ToArray();
            result.o_Conditions = lConditions.ToArray();
            result.o_GroupBy = lGroupBy.ToArray();
            result.o_SortBy = lSortBy.ToArray();

            return result;
        }

        public QueryBuilder()
        {
            this._tables = new List<IQueryTable>();
            this._columns = new List<IQueryColumn>();
            this._conditions = new List<IQueryCondition>();
            this._grouping = new List<IQueryGroup>();
            this._sorting = new List<IQuerySort>();

            this._limit = new QueryLimit() { i_RecordLimit = 0, i_StartingIndex = 0 };

            this.def = Plink.Definitions.Retrieve();

            this.Table<M>();
        }

        public IEnumerable<IQueryTable> Tables
        {
            get
            {
                return this._tables.AsEnumerable();
            }
        }
        public IEnumerable<IQueryColumn> Columns
        {
            get
            {
                return this._columns.AsEnumerable();
            }
        }
        public IEnumerable<IQueryCondition> Conditions
        {
            get
            {
                return this._conditions.AsEnumerable();
            }
        }
        public IEnumerable<IQueryGroup> Groups
        {
            get
            {
                return this._grouping.AsEnumerable();
            }
        }
        public IEnumerable<IQuerySort> Sorts
        {
            get
            {
                return this._sorting.AsEnumerable();
            }
        }
        public IQueryLimit Limit
        {
            get
            {
                return this._limit;
            }
        }

        public bool IsLocked
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IQueryBuilder<M> Table<T>()
        {
            this._tables.Add(new QueryTable()
            {
                s_Identifier = def.GetModule<T>().Identifier
            });
            return this;
        }


        #region " Columns "
        private const QueryColumnActions d_action = QueryColumnActions.None;
        private const string d_alias = null;

        public IQueryBuilder<M> Column<T>(Expression<Func<T, object>> expr, QueryColumnActions action, string alias) where T : IModel
        {
            this.Table<T>();
            this._columns.Add(new QueryColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Action = action,
                s_Alias = alias
            });
            return this;
        }

        public IQueryBuilder<M> Column<T>(Expression<Func<T, object>> expr) where T : IModel
        {
            this.Table<T>();
            this._columns.Add(new QueryColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Action = d_action,
                s_Alias = d_alias
            });
            return this;
        }

        public IQueryBuilder<M> Column<T>(Expression<Func<T, object>> expr, QueryColumnActions action) where T : IModel
        {
            this.Table<T>();
            this._columns.Add(new QueryColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Action = action,
                s_Alias = d_alias
            });
            return this;
        }

        public IQueryBuilder<M> Column<T>(Expression<Func<T, object>> expr, string alias) where T : IModel
        {
            this.Table<T>();
            this._columns.Add(new QueryColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Action = d_action,
                s_Alias = alias
            });
            return this;
        }

        public IQueryBuilder<M> Column(Expression<Func<M, object>> expr, QueryColumnActions action, string alias)
        {
            this._columns.Add(new QueryColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Action = action,
                s_Alias = alias
            });
            return this;
        }

        public IQueryBuilder<M> Column(Expression<Func<M, object>> expr)
        {
            this._columns.Add(new QueryColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Action = d_action,
                s_Alias = d_alias
            });
            return this;
        }

        public IQueryBuilder<M> Column(Expression<Func<M, object>> expr, QueryColumnActions action)
        {
            this._columns.Add(new QueryColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Action = action,
                s_Alias = d_alias
            });
            return this;
        }

        public IQueryBuilder<M> Column(Expression<Func<M, object>> expr, string alias)
        {
            this._columns.Add(new QueryColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Action = d_action,
                s_Alias = alias
            });
            return this;
        }
        #endregion

        #region " Groups "
        public IQueryBuilder<M> Group<T>(Expression<Func<T, object>> expr) where T : IModel
        {
            this.Table<T>();
            this._grouping.Add(new QueryGroup()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Group(Expression<Func<M, object>> expr)
        {
            this._grouping.Add(new QueryGroup()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier
            });
            return this;
        }
        #endregion

        #region " Having "
        private const QueryLogicalOperators d_Logical = QueryLogicalOperators.LogicalAnd;
        private const QueryComparisonOperators d_Operator = QueryComparisonOperators.EqualTo;
        private const int d_depth = 0;
        private const string d_wildcard = "*";

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, int Depth, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, int Depth, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, int Depth)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, int Depth)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, int Depth, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, int Depth, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard)
        {
            this._conditions.Add(new QueryConditionActionableByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, int Depth, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn,
            QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, int Depth)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, int Depth, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = d_action,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, QueryColumnActions Action)
        {
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Action = Action,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionActionableByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Action = d_action,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }
        #endregion

        #region " Limit "
        public IQueryBuilder<M> SetLimit(int RecordLimit)
        {
            this._limit = new QueryLimit()
            {
                i_RecordLimit = RecordLimit,
                i_StartingIndex = 0
            };
            return this;
        }

        public IQueryBuilder<M> SetLimit(int RecordLimit, int StartingIndex)
        {
            this._limit = new QueryLimit()
            {
                i_RecordLimit = RecordLimit,
                i_StartingIndex = StartingIndex
            };
            return this;
        }
        #endregion

        #region " Sort "
        private const QuerySortDirections d_defaultdirection = QuerySortDirections.Default;
        public IQueryBuilder<M> Sort<T>(Expression<Func<T, object>> expr, QuerySortDirections direction) where T : IModel
        {
            this.Table<T>();
            this._sorting.Add(new QuerySort()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Direction = direction
            });
            return this;
        }

        public IQueryBuilder<M> Sort<T>(Expression<Func<T, object>> expr) where T : IModel
        {
            this.Table<T>();
            this._sorting.Add(new QuerySort()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Direction = QuerySortDirections.Default
            });
            return this;
        }

        public IQueryBuilder<M> Sort(Expression<Func<M, object>> expr)
        {
            this._sorting.Add(new QuerySort()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Direction = QuerySortDirections.Default
            });
            return this;
        }

        public IQueryBuilder<M> Sort(Expression<Func<M, object>> expr, QuerySortDirections direction)
        {
            this._sorting.Add(new QuerySort()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Direction = direction
            });
            return this;
        }
        #endregion

        #region " Where "
        /*private const QueryLogicalOperators d_logicaloperator = QueryLogicalOperators.LogicalAnd;
        private const QueryComparisonOperators d_comparison = QueryComparisonOperators.EqualTo;
        private const int d_depth = 0;
        private const string d_wildcard = "*";*/

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, int Depth)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, int Depth)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = d_Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = d_wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard)
        {
            this._conditions.Add(new QueryConditionByValue()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                o_Value = Value,
                s_Wildcard = Wildcard
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator) where T : IModel
        {
            this.Table<T>();
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<T>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn)
        {
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, int Depth)
        {
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator)
        {
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical)
        {
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = d_Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth)
        {
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = d_Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth)
        {
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = d_Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator)
        {
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = d_depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth)
        {
            this._conditions.Add(new QueryConditionByColumn()
            {
                s_Identifier = def.GetProperty<M>(expr).Identifier,
                e_Logical = Logical,
                e_Operator = Operator,
                i_Depth = Depth,
                s_ValueColumn = def.GetProperty<M>(valueColumn).Identifier
            });
            return this;
        }

        public void Lock()
        {
            throw new NotImplementedException();
        }

        public IBuilder<IQuery> Save()
        {
            throw new NotImplementedException();
        }

        public IBuilder<IQuery> Load()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
