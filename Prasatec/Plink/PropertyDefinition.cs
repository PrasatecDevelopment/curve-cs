using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Plink
{
    public sealed class PropertyDefinition
    {
        public PropertyInfo Reference { get; private set; }
        public Type Parent { get { return Reference.ReflectedType; } }
        public DeploymentDefinition Deployment { get { return Definitions.Instance.GetDeployment(Parent.Assembly); } }

        public String Identifier { get { return Definitions.Instance.GetModule(Parent).Identifier + Reference.Name; } }

        internal PropertyDefinition(PropertyInfo Reference)
        {
            this.Reference = Reference;
        }

        public object GetValue(object Source)
        {
            return this.Reference.GetValue(Source);
        }

        public Boolean? RadenAllowNull
        {
            get
            {
                return Reference.GetCustomAttribute<ColumnAllowNullAttribute>()?.Value;
            }
        }
        public ColumnAutoValues? RadenAutoValue
        {
            get
            {
                return Reference.GetCustomAttribute<ColumnAutoValueAttribute>()?.Value;
            }
        }
        public ColumnIndexTypes? RadenIndexType
        {
            get
            {
                return Reference.GetCustomAttribute<ColumnIndexAttribute>()?.Value;
            }
        }
        public String RadenColumnName
        {
            get
            {
                return Reference.GetCustomAttribute<ColumnPropertyAttribute>()?.Name;
            }
        }
        public Boolean? RadenHasDefaultValue
        {
            get
            {
                return Reference.GetCustomAttribute<ValueTypeAttribute>()?.DefaultSet;
            }
        }
        public Object RadenDefaultValue
        {
            get
            {
                return Reference.GetCustomAttribute<ValueTypeAttribute>()?.Default;
            }
        }
        public ValueTypeAttribute RadenValueType
        {
            get
            {
                return Reference.GetCustomAttribute<ValueTypeAttribute>();
            }
        }
    }
}
