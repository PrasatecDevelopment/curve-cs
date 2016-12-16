using Prasatec.Plink;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Raden
{
    internal static class EngineHelper
    {
        private static Definitions def;
        static EngineHelper()
        {
            def = Definitions.Retrieve();
        }
        internal static IDatabaseBackup createBackup(IConnection connection, params Type[] Models)
        {
            DatabaseBackup result = new DatabaseBackup();
            result.o_Structure = null;
            result.b_Successful = false;

            return result;
        }
        internal static IDatabaseRestore restoreFromBackup(IConnection connection, IDatabaseBackup Backup)
        {
            DatabaseRestore result = null;
            result.b_Successful = false;

            return result;
        }

        public static IResult compareToStructure(IConnection connection, IDatabaseStructure Structure)
        {
            throw new NotImplementedException();
        }

        internal static M ConvertToModel<M>(IDataReader reader)
        {
            object value = null;
            M record = (M)Activator.CreateInstance(typeof(M), BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, null, null);
            foreach (var property in def.GetProperties<M>().Where(x => x.RadenColumnName != null && x.RadenValueType != null))
            {
                value = reader[property.RadenColumnName];
                if (value is DBNull)
                {
                    value = null;
                }
                if (property.RadenValueType is ValueTypeForeignKeyAttribute || property.RadenValueType is ValueTypeTimestampAttribute)
                {
                    value = Activator.CreateInstance(property.Reference.PropertyType, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { value }, null);
                }
                property.Reference.SetValue(record, value);
            }
            try
            {
                var property = def.GetProperties<M>().Where(x => x.Reference.Name == "ID").FirstOrDefault();
                if (property != null)
                {
                    property.Reference.SetValue(record, reader[def.GetModule<M>().RadenTableIdColumn]);
                }
                property = null;
                //reader[def.GetModule<M>().RadenTableIdColumn];
            }
            catch { }
            return record;
        }

        internal static string getOperator(QueryComparisonOperators op)
        {
            switch (op)
            {
                case QueryComparisonOperators.EqualTo: goto default;
                case QueryComparisonOperators.GreaterOrEqual: return ">=";
                case QueryComparisonOperators.GreaterThan: return ">";
                case QueryComparisonOperators.IsLike: return "IS LIKE";
                case QueryComparisonOperators.IsNotLike: return "IS NOT LIKE";
                case QueryComparisonOperators.IsNotNull: return "IS NOT NULL";
                case QueryComparisonOperators.IsNull: return "IS NULL";
                case QueryComparisonOperators.LessOrEqual: return "<=";
                case QueryComparisonOperators.LessThan: return "<";
                case QueryComparisonOperators.NotEqualTo: return "!=";
                default:
                    return "=";
            }
        }

        internal static void queryCount<M>(ref QueryResult<M> result, IDbCommand command)
            where M : IModel
        {
            result.o_ScalarValue = command.ExecuteScalar();
            result.i_CountTotal = (int)Convert.ChangeType(result.ScalarValue, typeof(int));
            result.b_Successful = true;
        }
        internal static void querySelect<M>(ref QueryResult<M> result, IQuery query, IDbCommand command, IConnection source)
            where M : IModel
        {
            List<M> records = new System.Collections.Generic.List<M>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    records.Add(EngineHelper.ConvertToModel<M>(reader));
                }
            }
            result.i_CountRecords = records.Count;
            result.o_Records = records.ToArray();
            records.Clear(); records = null;
            if (query.RecordLimit > 0 && result.CountRecords == query.RecordLimit)
            {
                ((Query)query).i_RecordLimit = 0;
                ((Query)query).i_StartingRecord = 0;
                result.i_CountTotal = source.Count<M>(query).CountTotal;
            }
            else
            {
                result.i_CountTotal = result.CountRecords;
            }
            if (result.CountRecords > 0)
            {
                result.i_CountPages = (int)Math.Ceiling((double)(result.CountTotal / result.CountRecords));
            }
            else { result.i_CountPages = 1; }
            result.b_Successful = true;
        }

        internal static void queryNonQuery<M>(ref QueryResult<M> result, IDbCommand command)
            where M : IModel
        {
            result.i_AffectedRows = command.ExecuteNonQuery();
            result.b_Successful = true;
        }
    }
}
