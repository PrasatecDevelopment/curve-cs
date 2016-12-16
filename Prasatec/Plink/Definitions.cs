using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Plink
{
    public class Definitions
    {
        #region " Definition Helpers "
        internal static string GetNameFromExpressions<T>(Expression<Func<T, object>> expr)
        {
            var result = expr.Body.ToString();
            while (result.Contains("(") && result.IndexOf(".") > result.IndexOf("("))
            {
                result = result.Substring(result.IndexOf("(") + 1);
            }
            result = result.Substring(result.IndexOf(".") + 1);
            if (result.Contains(" "))
            {
                result = result.Substring(0, result.IndexOf(" "));
            }
            while (result.Contains(")"))
            {
                result = result.Substring(0, result.IndexOf(")"));
            }
            return result;
        }
        #endregion

        #region " Static Only Creation "
        internal static Definitions Instance { get; private set; }

        public static Definitions Retrieve()
        {
            if (Instance == null)
            {
                Instance = new Definitions();
            }
            return Instance;
        }
        #endregion

        private List<DeploymentDefinition> i_Deployments;
        private List<ModuleDefinition> i_Modules;
        private List<MethodDefinition> i_Methods;
        private List<PropertyDefinition> i_Properties;
        private List<FieldDefinition> i_Fields;

        private Definitions()
        {
            this.i_Deployments = new List<DeploymentDefinition>();
            this.i_Modules = new List<Plink.ModuleDefinition>();
            this.i_Methods = new List<Plink.MethodDefinition>();
            this.i_Properties = new List<Plink.PropertyDefinition>();
            this.i_Fields = new List<Plink.FieldDefinition>();
        }

        #region " Deployments "
        public DeploymentDefinition GetDeployment(Assembly Reference)
        {
            try
            {
                if (i_Deployments.Count(x => x.Reference == Reference) == 0)
                {
                    i_Deployments.Add(new DeploymentDefinition(Reference));
                }
                return i_Deployments.Where(x => x.Reference == Reference).FirstOrDefault();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return null;
            }
        }

        public DeploymentDefinition GetCallingDeployment() { return this.GetDeployment(Assembly.GetCallingAssembly()); }
        public DeploymentDefinition GetEntryDeployment() { return this.GetDeployment(Assembly.GetEntryAssembly()); }
        public DeploymentDefinition GetExecutingDeployment() { return this.GetDeployment(Assembly.GetExecutingAssembly()); }
        
        public IEnumerable<DeploymentDefinition> GetDeployments()
        {
            Assembly[] asm = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in asm)
            {
                yield return this.GetDeployment(a);
            }
            asm = null;
        }
        #endregion

        #region " Modules "
        public ModuleDefinition GetModule(Type Reference)
        {
            try
            {
                if (i_Modules.Count(x => x.Reference == Reference) == 0)
                {
                    i_Modules.Add(new ModuleDefinition(Reference));
                }
                return i_Modules.Where(x => x.Reference == Reference).FirstOrDefault();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return null;
            }
        }
        public IEnumerable<ModuleDefinition> GetModules(DeploymentDefinition Deployment)
        {
            return Deployment.GetModules();
        }
        public ModuleDefinition GetModule<T>()
        {
            return this.GetModule(typeof(T));
        }
        public IEnumerable<ModuleDefinition> GetModules(Assembly Reference)
        {
            return this.GetModules(this.GetDeployment(Reference));
        }
        public ModuleDefinition GetModule(String Identifier)
        {
            return i_Modules.Where(x => x.Identifier == Identifier).FirstOrDefault();
        }
        public IEnumerable<ModuleDefinition> GetAllModules()
        {
            foreach (DeploymentDefinition deployment in this.GetDeployments())
            {
                foreach (ModuleDefinition module in deployment.GetModules())
                {
                    yield return module;
                }
            }
        }
        #endregion

        #region " Methods "
        public MethodDefinition GetMethod(Type Parent, String Name)
        {
            try
            {
                MethodInfo Reference = Parent.GetMethod(Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (i_Methods.Count(x => x.Reference == Reference) == 0)
                {
                    i_Methods.Add(new MethodDefinition(Reference));
                }
                return i_Methods.Where(x => x.Reference == Reference).FirstOrDefault();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return null;
            }
        }
        public IEnumerable<MethodDefinition> GetMethods(Type Reference)
        {
            return this.GetModule(Reference).GetMethods();
        }
        public MethodDefinition GetMethod<T>(Expression<Func<T, object>> expr)
        {
            return this.GetMethod(typeof(T), Definitions.GetNameFromExpressions<T>(expr));
        }
        public IEnumerable<MethodDefinition> GetMethods<T>()
        {
            return this.GetMethods(typeof(T));
        }
        public MethodDefinition GetMethod(String Identifier)
        {
            return i_Methods.Where(x => x.Identifier == Identifier).FirstOrDefault();
        }
        #endregion

        #region " Properties "
        public PropertyDefinition GetProperty(Type Parent, String Name)
        {
            try
            {
                  PropertyInfo Reference = Parent.GetProperty(Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (i_Properties.Count(x => x.Reference == Reference) == 0)
                {
                    i_Properties.Add(new PropertyDefinition(Reference));
                }
                return i_Properties.Where(x => x.Reference == Reference).FirstOrDefault();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return null;
            }
        }
        public IEnumerable<PropertyDefinition> GetProperties(Type Reference)
        {
            return this.GetModule(Reference).GetProperties();
        }
        public PropertyDefinition GetProperty<T>(Expression<Func<T, object>> expr)
        {
            return this.GetProperty(typeof(T), Definitions.GetNameFromExpressions<T>(expr));
        }
        public IEnumerable<PropertyDefinition> GetProperties<T>()
        {
            return this.GetProperties(typeof(T));
        }
        public PropertyDefinition GetProperty(String Identifier)
        {
            return i_Properties.Where(x => x.Identifier == Identifier).FirstOrDefault();
        }
        #endregion

        #region " Fields "
        public FieldDefinition GetField(Type Parent, String Name)
        {
            try
            {
                FieldInfo Reference = Parent.GetField(Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (i_Fields.Count(x => x.Reference == Reference) == 0)
                {
                    i_Fields.Add(new FieldDefinition(Reference));
                }
                return i_Fields.Where(x => x.Reference == Reference).FirstOrDefault();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return null;
            }
        }
        public IEnumerable<FieldDefinition> GetFields(Type Reference)
        {
            return this.GetModule(Reference).GetFields();
        }
        public FieldDefinition GetField<T>(Expression<Func<T, object>> expr)
        {
            return this.GetField(typeof(T), Definitions.GetNameFromExpressions<T>(expr));
        }
        public IEnumerable<FieldDefinition> GetFields<T>()
        {
            return this.GetFields(typeof(T));
        }
        public FieldDefinition GetField(String Identifier)
        {
            return i_Fields.Where(x => x.Identifier == Identifier).FirstOrDefault();
        }
        #endregion
    }
}
