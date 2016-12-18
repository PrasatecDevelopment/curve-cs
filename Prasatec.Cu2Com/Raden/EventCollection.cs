using Prasatec.Raden;

namespace Prasatec.Cu2Com.Raden
{
    public sealed class EventCollection : BaseCollection<EventModel>
    {
        private static string[] COLUMNS;

        public EventCollection(IConnection Connection) : base(Connection)
        {
            COLUMNS = new string[] { "Code", "Type", "Timestamp" };
        }

        public EventCollection(IConnection Connection, IQuery Query) : base(Connection, Query)
        {
            COLUMNS = new string[] { "Code", "Type", "Timestamp" };
        }

        protected override IQueryBuilder<EventModel> CreateQueryBuilder()
        {
            return new QueryBuilder<EventModel>();
        }

        protected override string[] GetColumns()
        {
            return COLUMNS;
        }
    }
}

