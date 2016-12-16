using Prasatec.Experience;
using Prasatec.Experience.DynamicWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Interactions
{
    public sealed class ManageLogsController
    {
        private IWindowCollection Window { get; set; }

        public ManageLogsController(ManageLogsController Collection)
        {
            this.Window = new CollectionWindow() { Text = "Log Event Collection" };
        }
    }
}