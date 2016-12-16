using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Plink
{
    public sealed class MethodDefinition
    {
        public MethodInfo Reference { get; private set; }
        public Type Parent { get { return Reference.DeclaringType; } }
        public DeploymentDefinition Deployment { get { return Definitions.Instance.GetDeployment(Parent.Assembly); } }

        public String Identifier { get { return Definitions.Instance.GetModule(Parent).Identifier + Reference.Name; } }

        internal MethodDefinition(MethodInfo Reference)
        {
            this.Reference = Reference;
        }
    }
}
