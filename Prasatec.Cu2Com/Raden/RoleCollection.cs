using Prasatec.Raden;

namespace Prasatec.Cu2Com.Raden
{
    public sealed class RoleCollection : BaseCollection<RoleModel>
    {
        private static string[] COLUMNS;

        public RoleCollection(IConnection Connection) : base(Connection)
        {
            COLUMNS = new string[] { "Name", "User Mode" };
        }

        public RoleCollection(IConnection Connection, IQuery Query) : base(Connection, Query)
        {
            COLUMNS = new string[] { "Name", "User Mode" };
        }

        protected override IQueryBuilder<RoleModel> CreateQueryBuilder()
        {
            return new QueryBuilder<RoleModel>();
        }

        protected override string[] GetColumns()
        {
            return COLUMNS;
        }
    }
}

