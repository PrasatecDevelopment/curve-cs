using Prasatec.Raden;

namespace Prasatec.Cu2Com.Raden
{
    public sealed class DelegationCollection : BaseCollection<DelegationModel>
    {
        private static string[] COLUMNS;

        public DelegationCollection(IConnection Connection) : base(Connection)
        {
            COLUMNS = new string[] { "Manager", "Subordinate" };
        }

        public DelegationCollection(IConnection Connection, IQuery Query) : base(Connection, Query)
        {
            COLUMNS = new string[] { "Manager", "Subordinate" };
        }

        protected override IQueryBuilder<DelegationModel> CreateQueryBuilder()
        {
            return new QueryBuilder<DelegationModel>();
        }

        protected override string[] GetColumns()
        {
            return COLUMNS;
        }
    }
}

