using System;
using Prasatec.Raden;

namespace Prasatec.Cu2Com.Raden
{
    public sealed class RoleCollection : ICollection<RoleModel>
    {
        private IConnection Connection;
        private const int DEFAULT_LIMIT = 25;
        public RoleCollection(IConnection Connection)
            : this(Connection, new QueryBuilder<RoleModel>().Build())
        {

        }
        public RoleCollection(IConnection Connection, IQuery Query)
        {
            this.baseQuery = Query;
            this.Connection = Connection;
        }
        public string[] Columns
        {
            get
            {
                return new string[]
                {
                    "Name",
                    "User Mode"
                };
            }
        }

        
        private int i_PageIndex, i_RecordsPerPage;
        private IQuery baseQuery;

        public event EventHandler ListRefreshed;

        public void Refresh()
        {
            var result = Connection.List<RoleModel>(this.baseQuery);
            this.Records = result.Records;
            this.ListRefreshed?.Invoke(this, EventArgs.Empty);
        }
        public RoleModel[] Records { get; private set; }

        public int PageCount { get; private set; }
        public int PageIndex
        {
            get { return this.i_PageIndex; }

            set
            {
                if (value > this.PageCount) { value = PageCount; }
                if (value < 1) { value = 1; }
                this.Refresh();
            }
        }

        public int RecordsPerPage
        {
            get
            {
                return this.i_RecordsPerPage;
            }

            set
            {
                this.i_RecordsPerPage = value;
                this.i_PageIndex = 1;
                this.Refresh();
            }
        }

        public bool Next()
        {
            if (this.PageIndex != this.PageCount)
            {
                this.PageIndex++;
                return true;
            }
            return false;
        }

        public bool Previous()
        {
            if (this.PageIndex != 1)
            {
                this.PageIndex--;
                return true;
            }
            return false;
        }

        public void FirstPage()
        {
            if (this.PageIndex != 1)
            {
                this.PageIndex = 1;
            }
        }

        public void LastPage()
        {
            if (this.PageIndex != this.PageCount)
            {
                this.PageIndex = this.PageCount;
            }
        }

        public void Reload()
        {
            this.i_PageIndex = 1;
            this.i_RecordsPerPage = DEFAULT_LIMIT;
            this.Refresh();
        }
    }
}

