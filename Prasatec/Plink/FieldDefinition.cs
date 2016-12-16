using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Plink
{
    public sealed class FieldDefinition
    {
        public FieldInfo Reference { get; private set; }
        public Type Parent { get { return Reference.DeclaringType; } }
        public DeploymentDefinition Deployment { get { return Definitions.Instance.GetDeployment(Parent.Assembly); } }

        public String Identifier { get { return Definitions.Instance.GetModule(Parent).Identifier + Reference.Name; } }

        internal FieldDefinition(FieldInfo Reference)
        {
            this.Reference = Reference;
        }
    }
}
