using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Plink
{
    public sealed class DeploymentDefinition
    {
        public Assembly Reference { get; private set; }

        internal DeploymentDefinition(Assembly Reference)
        {
            this.Reference = Reference;
        }

        public IEnumerable<ModuleDefinition> GetModules()
        {
            foreach (var result in Reference.GetTypes().Select(x => Definitions.Instance.GetModule(x)))
            {
                yield return result;
            }
        }

        public String Company
        {
            get
            {
                return Reference.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
            }
        }

        public String Configuration
        {
            get
            {
                return Reference.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;
            }
        }

        public String Copyright
        {
            get
            {
                return Reference.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
            }
        }

        public String Culture
        {
            get
            {
                return Reference.GetCustomAttribute<AssemblyCultureAttribute>()?.Culture;
            }
        }

        public String Description
        {
            get
            {
                return Reference.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
            }
        }

        public Version Version
        {
            get
            {
                return Version.Parse(Reference.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version);
            }
        }

        public String Product
        {
            get
            {
                return Reference.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
            }
        }

        public String Title
        {
            get
            {
                return Reference.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            }
        }

        public String Trademark
        {
            get
            {
                return Reference.GetCustomAttribute<AssemblyTrademarkAttribute>()?.Trademark;
            }
        }

        public Version ProductVersion
        {
            get
            {
                return Reference.GetName().Version;
            }
        }

        public String UrlFeatureSuggestion
        {
            get
            {
                return Reference.GetCustomAttribute<UrlFeatureSuggestionAttribute>()?.Url;
            }
        }

        public String UrlKnowledgeBase
        {
            get
            {
                return Reference.GetCustomAttribute<UrlKnowledgeBaseAttribute>()?.Url;
            }
        }

        public String UrlOnlineGuide
        {
            get
            {
                return Reference.GetCustomAttribute<UrlOnlineGuideAttribute>()?.Url;
            }
        }

        public String UrlProductHomepage
        {
            get
            {
                return Reference.GetCustomAttribute<UrlProductHomepageAttribute>()?.Url;
            }
        }

        public String Filename
        {
            get
            {
                return Reference.Location;
            }
        }

        public String Identifier
        {
            get
            {
                return Reference.FullName;
            }
        }
    }
}
