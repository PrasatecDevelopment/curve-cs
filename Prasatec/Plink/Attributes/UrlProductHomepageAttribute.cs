using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Plink
{
    public class UrlProductHomepageAttribute : Attribute
    {
        public String Url { get; private set; }
        public UrlProductHomepageAttribute(String Url)
        {
            this.Url = Url;
        }
    }
}
