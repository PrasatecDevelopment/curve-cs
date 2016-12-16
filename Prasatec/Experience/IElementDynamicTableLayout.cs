using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience
{
    public interface IElementDynamicTableLayout
    {
        event EventHandler<GenericEventArgs<string>> DropdownChanged;
        int Count { get; }
        DynamicEditorModes Mode { get; set; }
        bool Enabled { get; set; }
        bool Visible { get; set; }

        void SetValue(string Name, object Value);
        object GetValue(string Name);
        T GetValue<T>(string Name);
        void AddTextbox(string Name, string Label, string Value);
        void AddPassword(string Name, string Label, string Value);
        void AddNumber(string Name, string Label, decimal Minimum, decimal Maximum, decimal Value);
        void AddDropdown(string Name, string Label, object[] List, int Value);
        void AddTimestamp(string Name, string Label, DateTime Value);
        void AddContent(string Name, string Label, string Value);
        void Reset();
        void RemoveControl(string Name);
    }
}
