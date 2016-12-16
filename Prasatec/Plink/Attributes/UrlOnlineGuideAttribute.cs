using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Plink
{
    public class UrlOnlineGuideAttribute : Attribute
    {
        public String Url { get; private set; }
        public UrlOnlineGuideAttribute(String Url)
        {
            this.Url = Url;
        }
    }
}
