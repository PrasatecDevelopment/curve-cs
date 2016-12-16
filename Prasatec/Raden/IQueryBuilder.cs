using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    public interface IQueryBuilder<M> : IBuilder<IQuery>
        where M : IModel
    {
        IEnumerable<IQueryTable> Tables { get; }
        IEnumerable<IQueryColumn> Columns { get; }
        IEnumerable<IQueryCondition> Conditions { get; }
        IEnumerable<IQueryGroup> Groups { get; }
        IEnumerable<IQuerySort> Sorts { get; }
        IQueryLimit Limit { get; }

        IQueryBuilder<M> Column(Expression<Func<M, object>> expr);
        IQueryBuilder<M> Column(Expression<Func<M, object>> expr, string alias);
        IQueryBuilder<M> Column(Expression<Func<M, object>> expr, QueryColumnActions action);
        IQueryBuilder<M> Column(Expression<Func<M, object>> expr, QueryColumnActions action, string alias);
        IQueryBuilder<M> Column<T>(Expression<Func<T, object>> expr) where T : IModel;
        IQueryBuilder<M> Column<T>(Expression<Func<T, object>> expr, string alias) where T : IModel;
        IQueryBuilder<M> Column<T>(Expression<Func<T, object>> expr, QueryColumnActions action) where T : IModel;
        IQueryBuilder<M> Column<T>(Expression<Func<T, object>> expr, QueryColumnActions action, string alias) where T : IModel;
        IQueryBuilder<M> Group(Expression<Func<M, object>> expr);
        IQueryBuilder<M> Group<T>(Expression<Func<T, object>> expr) where T : IModel;
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, int Depth);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, int Depth);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, int Depth, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, int Depth);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, int Depth, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, int Depth, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action);
        IQueryBuilder<M> Having(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard, QueryColumnActions Action);
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, int Depth) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, int Depth) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, int Depth, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, int Depth) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, int Depth, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, int Depth, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> Having<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard, QueryColumnActions Action) where T : IModel;
        IQueryBuilder<M> SetLimit(int RecordLimit);
        IQueryBuilder<M> SetLimit(int RecordLimit, int StartingIndex);
        IQueryBuilder<M> Sort(Expression<Func<M, object>> expr);
        IQueryBuilder<M> Sort(Expression<Func<M, object>> expr, QuerySortDirections direction);
        IQueryBuilder<M> Sort<T>(Expression<Func<T, object>> expr) where T : IModel;
        IQueryBuilder<M> Sort<T>(Expression<Func<T, object>> expr, QuerySortDirections direction) where T : IModel;
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, int Depth);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, int Depth);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, int Depth);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth);
        IQueryBuilder<M> Where(Expression<Func<M, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard);
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, int Depth) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, int Depth) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, string Wildcard) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, int Depth) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, int Depth) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryComparisonOperators Operator, int Depth) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, string Wildcard) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryComparisonOperators Operator, int Depth, string Wildcard) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, Expression<Func<M, object>> valueColumn, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth) where T : IModel;
        IQueryBuilder<M> Where<T>(Expression<Func<T, object>> expr, object Value, QueryLogicalOperators Logical, QueryComparisonOperators Operator, int Depth, string Wildcard) where T : IModel;
    }
}
