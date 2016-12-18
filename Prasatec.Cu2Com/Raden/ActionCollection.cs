using Prasatec.Raden;

namespace Prasatec.Cu2Com.Raden
{
    public sealed class ActionCollection : BaseCollection<ActionModel>
    {
        private static string[] COLUMNS;

        public ActionCollection(IConnection Connection) : base(Connection)
        {
            COLUMNS = new string[] { "User ID", "Action", "Performed At" };
        }

        public ActionCollection(IConnection Connection, IQuery Query) : base(Connection, Query)
        {
            COLUMNS = new string[] { "User ID", "Action", "Performed At" };
        }

        protected override IQueryBuilder<ActionModel> CreateQueryBuilder()
        {
            return new QueryBuilder<ActionModel>();
        }

        protected override string[] GetColumns()
        {
            return COLUMNS;
        }
    }
}

