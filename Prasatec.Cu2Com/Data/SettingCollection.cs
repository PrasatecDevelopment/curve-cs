using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Data
{
    class SettingCollection : Raden.ICollection<Setting>
    {
        private const int DEFAULT_LIMIT = 25;
        public SettingCollection()
            : this(new Raden.QueryBuilder<Setting>().Build())
        {

        }
        public SettingCollection(Raden.IQuery Query)
        {
            this.baseQuery = Query;
        }

        private int i_PageIndex, i_RecordsPerPage;
        private Raden.IQuery baseQuery;

        public event EventHandler ListRefreshed;

        public void Refresh()
        {
            this.ListRefreshed?.Invoke(this, EventArgs.Empty);
            throw new NotImplementedException();
        }

        public string[] Columns { get; private set; }
        public Setting[] Records { get; private set; }

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