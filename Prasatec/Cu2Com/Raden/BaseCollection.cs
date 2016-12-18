using Prasatec.Raden;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Raden
{
    public abstract class BaseCollection<M> : ICollection<M>
        where M : IModel
    {
        private int i_PageIndex, i_RecordsPerPage;

        protected IConnection Connection { get; private set; }
        protected IQuery BaseQuery { get; private set; }
        protected virtual int DefaultLimit { get { return 25; } }

        public int PageCount { get; private set; }
        public String[] Columns { get { return this.GetColumns(); } }
        public M[] Records { get; private set; }
        public event EventHandler ListRefreshed;

        protected abstract IQueryBuilder<M> CreateQueryBuilder();
        protected abstract String[] GetColumns();

        public BaseCollection(IConnection Connection)
        {
            this.Connection = Connection;
            this.BaseQuery = this.CreateQueryBuilder().Build();
            this.Reload();
        }
        public BaseCollection(IConnection Connection, IQuery Query)
        {
            this.Connection = Connection;
            this.BaseQuery = Query;
            this.Reload();
        }

        public void Refresh()
        {
            var result = Connection.List<M>(this.BaseQuery);
            this.Records = result.Records;
            this.ListRefreshed?.Invoke(this, EventArgs.Empty);
        }

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
            get { return i_RecordsPerPage; }
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
            if (this.PageIndex > 1)
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
            if (PageIndex != PageCount)
            {
                this.PageIndex = this.PageCount;
            }
        }

        public void Reload()
        {
            this.i_PageIndex = 1;
            this.i_RecordsPerPage = DefaultLimit;
            this.Refresh();
        }
    }
}
