using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Plink
{
    public sealed class ModuleDefinition
    {
        public Type Reference { get; private set; }
        public DeploymentDefinition Deployment { get { return Definitions.Instance.GetDeployment(Reference.Assembly); } }

        public String Identifier { get { return Deployment.Identifier + Reference.FullName; } }

        public IEnumerable<MethodDefinition> GetMethods()
        {
            foreach (var result in Reference.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => Definitions.Instance.GetMethod(Reference, x.Name)))
            {
                yield return result;
            }
        }

        public IEnumerable<PropertyDefinition> GetProperties()
        {
            foreach (var result in Reference.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => Definitions.Instance.GetProperty(Reference, x.Name)))
            {
                yield return result;
            }
        }

        public IEnumerable<FieldDefinition> GetFields()
        {
            foreach (var result in Reference.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => Definitions.Instance.GetField(Reference, x.Name)))
            {
                yield return result;
            }
        }

        internal ModuleDefinition(Type Reference)
        {
            this.Reference = Reference;
        }

        public String RadenTableName
        {
            get
            {
                return Reference.GetCustomAttribute<TableModelAttribute>()?.Name;
            }
        }
        public String RadenTableIdColumn
        {
            get
            {
                return Reference.GetCustomAttribute<TableModelAttribute>()?.IdColumn;
            }
        }
    }
}
