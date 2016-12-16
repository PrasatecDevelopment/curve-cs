using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience.Elements
{
    public enum DynamicControlTypes
    {
        Textbox,
        Password,
        Number,
        Dropdwn,
        Timestamp,
        Content
    }

    /*Int32 TextboxHeight { get; }
    Int32 PasswordHeight { get; }
    Int32 NumberHeight { get; }
    Int32 DropdownHeight { get; }
    Int32 TimestampHeight { get; }
    Int32 ContentHeight { get; }*/
    public class DynamicLayoutPanel : TableLayoutPanel
    {
        public void AddTextbox(string Name, string Label, String Value)
        {

        }
        public void AddPassword(string Name, string Label)
        {

        }
        public void AddNumber(string Name, string Label, decimal Value, decimal MinValue, decimal MaxValue)
        {

        }
        public void AddTimestamp(string Name, string Label, DateTime Value)
        {

        }
        public DynamicLayoutPanel()
        {
            this.MaximumSize = new System.Drawing.Size(10000, 0);
        }
    }
}
