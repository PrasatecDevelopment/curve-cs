using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience.Helpers
{
    public static class DesignHelper
    {
        public static Color GetAdjustedForeColor(Color c)
        {
            Console.WriteLine(c.GetBrightness());
            
            if (c.GetBrightness() > 0.75f)
            {
                return Color.Black;
            }
            return Color.White;
        }
    }
}
