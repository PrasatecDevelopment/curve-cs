using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using Prasatec.Plink;

namespace Prasatec.Cu2Com.Raden
{
    public abstract class BaseModel<M> : IModel
        where M : IModel
    {
        private List<string> changedProperties;
        public BaseModel()
        {
            this.changedProperties = new List<string>();
        }

        public abstract ulong ID { get; set; }

        public bool IsChanged(Expression<Func<M, object>> expr)
        {
            return this.changedProperties.Contains(Definitions.Instance.GetProperty<M>(expr).Reference.Name);
        }
        protected void setChanged(string Name)
        {
            if (Name.StartsWith("set_"))
            {
                Name = Name.Substring(Name.IndexOf("_") + 1);
                if (!this.changedProperties.Contains(Name))
                {
                    this.changedProperties.Add(Name);
                }
            }
        }
    }
}
