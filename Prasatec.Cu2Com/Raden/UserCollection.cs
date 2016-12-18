using Prasatec.Raden;

namespace Prasatec.Cu2Com.Raden
{
    public sealed class UserCollection : BaseCollection<UserModel>
    {
        private static string[] COLUMNS;

        public UserCollection(IConnection Connection) : base(Connection)
        {
            COLUMNS = new string[] { "User Login", "Display Name", "Role" };
        }

        public UserCollection(IConnection Connection, IQuery Query) : base(Connection, Query)
        {
            COLUMNS = new string[] { "User Login", "Display Name", "Role" };
        }

        protected override IQueryBuilder<UserModel> CreateQueryBuilder()
        {
            return new QueryBuilder<UserModel>();
        }

        protected override string[] GetColumns()
        {
            return COLUMNS;
        }
    }
}

