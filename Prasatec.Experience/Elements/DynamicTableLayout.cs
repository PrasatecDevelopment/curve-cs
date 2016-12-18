using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience.Elements
{
    public class DynamicTableLayout : TableLayoutPanel, IElementDynamicTableLayout
    {
        private DynamicEditorModes e_Mode;
        private List<Tuple<string, Label, Control>> elements;
        private Dictionary<string, object> values;

        public event EventHandler<GenericEventArgs<string>> DropdownChanged;

        public DynamicTableLayout()
        {
            this.values = new Dictionary<string, object>();
            this.RowStyles.Clear();
            this.ColumnStyles.Clear();
            this.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            this.BackColor = Color.Transparent;
            this.MaximumSize = new System.Drawing.Size(10000, 6);
            this.MinimumSize = new System.Drawing.Size(0, 6);
            this.elements = new List<Tuple<string, Label, Control>>();
            this.Mode = DynamicEditorModes.View;
            this.Padding = new Padding(0, 0, 0, 6);
            //this.Controls["table"].rows
        }

        public DynamicEditorModes Mode
        {
            get
            {
                return this.e_Mode;
            }

            set
            {
                if (value == DynamicEditorModes.View)
                {
                    this.values.Clear();
                }
                foreach (var element in this.elements)
                {
                    element.Item2.Visible = value == DynamicEditorModes.View;
                    element.Item3.Visible = value == DynamicEditorModes.Edit;
                    //element.Item2.Enabled = element.Item2.Visible;
                    //element.Item3.Enabled = element.Item3.Visible;
                    if (value == DynamicEditorModes.View)
                    {
                        this.values.Add(element.Item1, this.GetValue(element.Item1));
                    }
                }
                e_Mode = value;
            }
        }

        private void addElement(string Name, string Label, float Height, Label Viewer, Control Editor)
        {
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, Height));
            this.Controls.Add(new Label() {
                Text = Label + ":",
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.TopRight,
                Margin = new Padding(0, 0, 3, 0),
                Padding = new Padding(0, 8, 0, 0)
            }, 0, this.RowStyles.Count - 1);
            this.Controls.Add(new Panel()
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 0, 0, 0),
                Name = Name
            }, 1, this.RowStyles.Count - 1);
            this.GetControlFromPosition(1, this.RowStyles.Count - 1).Controls.Add(Viewer);
            this.GetControlFromPosition(1, this.RowStyles.Count - 1).Controls.Add(Editor);
            Viewer.Visible = this.Mode == DynamicEditorModes.View; //Viewer.Enabled = Viewer.Visible;
            Editor.Visible = !Viewer.Visible; //Editor.Enabled = Editor.Visible;
            decimal paddingSize = (this.GetControlFromPosition(1, this.RowStyles.Count - 1).Height - Editor.Height + (Editor.Margin.Top + Editor.Margin.Bottom)) / 2;
            GetControlFromPosition(1, this.RowStyles.Count - 1).Padding = new Padding(0, (int)Math.Floor(paddingSize), 0, (int)Math.Ceiling(paddingSize));
            this.MaximumSize = new Size(this.MaximumSize.Width, this.MaximumSize.Height + (int)Height);
            this.MinimumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height + (int)Height);
            this.Size = new Size(this.Size.Width, this.Size.Height + (int)Height);
            if (this.FindForm() != null)
            {
                if (this.FindForm().MaximumSize.Height > 0)
                {
                    this.FindForm().MaximumSize = new Size(this.FindForm().MaximumSize.Width, this.FindForm().MaximumSize.Height + (int)Height);
                }
                if (this.FindForm().MinimumSize.Height > 0)
                {
                    this.FindForm().MinimumSize = new Size(this.FindForm().MinimumSize.Width, this.FindForm().MinimumSize.Height + (int)Height);
                }
                this.FindForm().Size = new Size(this.FindForm().Size.Width, this.FindForm().Size.Height + (int)Height);
            }
        }

        public void AddContent(string Name, string Label, string Value)
        {
            if (this.elements.Where(x => x.Item1 == Name).Count() > 0) { throw new ArgumentException(Name + " already exists"); }
            int elementId = this.elements.Count;
            this.elements.Add(new Tuple<string, Label, Control>(Name, new Label(), new TextBox()));
            this.elements[elementId].Item2.Name = Name + "_label";
            this.elements[elementId].Item2.Dock = DockStyle.Fill;
            this.elements[elementId].Item2.TextAlign = ContentAlignment.TopLeft;
            this.elements[elementId].Item2.Padding = new Padding(0, 4, 0, 0);
            this.elements[elementId].Item2.AutoEllipsis = true;
            this.elements[elementId].Item2.AutoSize = false;
            this.elements[elementId].Item3.Name = Name + "_control";
            this.elements[elementId].Item3.Dock = DockStyle.Fill;
            this.elements[elementId].Item3.TextChanged += EditorChanged;
            this.elements[elementId].Item3.Text = Value;
            this.elements[elementId].Item3.Margin = new Padding(0, 6, 0, 6);
            ((TextBox)this.elements[elementId].Item3).Multiline = true;
            ((TextBox)this.elements[elementId].Item3).ScrollBars = ScrollBars.Vertical;
            this.addElement(Name, Label, 80f, this.elements[elementId].Item2, this.elements[elementId].Item3);
            this.values.Add(Name, Value);
        }

        public void AddDropdown(string Name, string Label, object[] List, int Value)
        {
            if (this.elements.Where(x => x.Item1 == Name).Count() > 0) { throw new ArgumentException(Name + " already exists"); }
            if (Value < -1) { Value = -1; }
            if (Value > List?.Count() - 1) { Value = -1; }
            int elementId = this.elements.Count;
            this.elements.Add(new Tuple<string, Label, Control>(Name, new Label(), new ComboBox()));
            this.elements[elementId].Item2.Name = Name + "_label";
            this.elements[elementId].Item2.Dock = DockStyle.Fill;
            this.elements[elementId].Item2.TextAlign = ContentAlignment.TopLeft;
            this.elements[elementId].Item2.Padding = new Padding(0, 4, 0, 0);
            this.elements[elementId].Item2.AutoEllipsis = true;
            this.elements[elementId].Item2.AutoSize = false;
            this.elements[elementId].Item3.Name = Name + "_control";
            this.elements[elementId].Item3.Dock = DockStyle.Fill;
            this.elements[elementId].Item3.TextChanged += EditorChanged;
            this.elements[elementId].Item3.Margin = new Padding(0);
            ((ComboBox)this.elements[elementId].Item3).DropDownStyle = ComboBoxStyle.DropDownList;
            ((ComboBox)this.elements[elementId].Item3).Items.AddRange(List);
            ((ComboBox)this.elements[elementId].Item3).SelectedIndex = Value;
            ((ComboBox)this.elements[elementId].Item3).SelectedIndexChanged += DynamicTableLayout_SelectedIndexChanged;
            this.addElement(Name, Label, 32f, this.elements[elementId].Item2, this.elements[elementId].Item3);
            this.values.Add(Name, Value);
        }

        public void AddNumber(string Name, string Label, decimal Minimum, decimal Maximum, decimal Value)
        {
            if (this.elements.Where(x => x.Item1 == Name).Count() > 0) { throw new ArgumentException(Name + " already exists"); }
            if (Maximum < Minimum + 1) { Maximum = Minimum + 1; }
            if (Value < Minimum) { Value = Minimum; }
            if (Value > Maximum) { Value = Maximum; }
            int elementId = this.elements.Count;
            this.elements.Add(new Tuple<string, Label, Control>(Name, new Label(), new NumericUpDown()));
            this.elements[elementId].Item2.Name = Name + "_label";
            this.elements[elementId].Item2.AutoEllipsis = true;
            this.elements[elementId].Item2.Dock = DockStyle.Fill;
            this.elements[elementId].Item2.TextAlign = ContentAlignment.MiddleLeft;
            this.elements[elementId].Item2.AutoSize = false;
            this.elements[elementId].Item3.Name = Name + "_control";
            this.elements[elementId].Item3.Dock = DockStyle.Fill;
            this.elements[elementId].Item3.TextChanged += EditorChanged;
            this.elements[elementId].Item3.Margin = new Padding(0);
            ((NumericUpDown)this.elements[elementId].Item3).Minimum = Minimum;
            ((NumericUpDown)this.elements[elementId].Item3).Maximum = Maximum;
            ((NumericUpDown)this.elements[elementId].Item3).Value = Value;
            this.addElement(Name, Label, 32f, this.elements[elementId].Item2, this.elements[elementId].Item3);
            this.values.Add(Name, Value);
        }

        public void AddPassword(string Name, string Label, string Value)
        {
            if (this.elements.Where(x => x.Item1 == Name).Count() > 0) { throw new ArgumentException(Name + " already exists"); }
            int elementId = this.elements.Count;
            this.elements.Add(new Tuple<string, Label, Control>(Name, new Label(), new TextBox()));
            this.elements[elementId].Item2.Name = Name + "_label";
            this.elements[elementId].Item2.AutoEllipsis = true;
            this.elements[elementId].Item2.Dock = DockStyle.Fill;
            this.elements[elementId].Item2.TextAlign = ContentAlignment.MiddleLeft;
            this.elements[elementId].Item2.AutoSize = false;
            this.elements[elementId].Item3.Name = Name + "_control";
            this.elements[elementId].Item3.Dock = DockStyle.Fill;
            this.elements[elementId].Item3.Margin = new Padding(0);
            ((TextBox)this.elements[elementId].Item3).UseSystemPasswordChar = true;
            this.elements[elementId].Item3.TextChanged += EditorChanged;
            this.elements[elementId].Item3.Text = Value;
            this.addElement(Name, Label, 32f, this.elements[elementId].Item2, this.elements[elementId].Item3);
            this.values.Add(Name, Value);
        }

        public void AddTextbox(string Name, string Label, string Value)
        {
            if (this.elements.Where(x => x.Item1 == Name).Count() > 0) { throw new ArgumentException(Name + " already exists"); }
            int elementId = this.elements.Count;
            this.elements.Add(new Tuple<string, Label, Control>(Name, new Label(), new TextBox()));
            this.elements[elementId].Item2.Name = Name + "_label";
            this.elements[elementId].Item2.AutoEllipsis = true;
            this.elements[elementId].Item2.Dock = DockStyle.Fill;
            this.elements[elementId].Item2.TextAlign = ContentAlignment.MiddleLeft;
            this.elements[elementId].Item2.AutoSize = false;
            this.elements[elementId].Item3.Name = Name + "_control";
            this.elements[elementId].Item3.Dock = DockStyle.Fill;
            this.elements[elementId].Item3.Margin = new Padding(0);
            this.elements[elementId].Item3.TextChanged += EditorChanged;
            this.elements[elementId].Item3.Text = Value;
            this.addElement(Name, Label, 32f, this.elements[elementId].Item2, this.elements[elementId].Item3);
            this.values.Add(Name, Value);
        }

        public void AddTimestamp(string Name, string Label, DateTime Value)
        {
            if (this.elements.Where(x => x.Item1 == Name).Count() > 0) { throw new ArgumentException(Name + " already exists"); }
            int elementId = this.elements.Count;
            this.elements.Add(new Tuple<string, Label, Control>(Name, new Label(), new DateTimePicker()));
            this.elements[elementId].Item2.Name = Name + "_label";
            this.elements[elementId].Item2.AutoEllipsis = true;
            this.elements[elementId].Item2.Dock = DockStyle.Fill;
            this.elements[elementId].Item2.TextAlign = ContentAlignment.MiddleLeft;
            this.elements[elementId].Item2.AutoSize = false;
            this.elements[elementId].Item3.Name = Name + "_control";
            this.elements[elementId].Item3.Dock = DockStyle.Fill;
            this.elements[elementId].Item3.Margin = new Padding(0);
            ((DateTimePicker)this.elements[elementId].Item3).ValueChanged += EditorChanged;
            ((DateTimePicker)this.elements[elementId].Item3).Value = Value;
            ((DateTimePicker)this.elements[elementId].Item3).Format = DateTimePickerFormat.Custom;
            ((DateTimePicker)this.elements[elementId].Item3).CustomFormat = "MMMM d, yyyy h:mm:ss tt";
            this.addElement(Name, Label, 32f, this.elements[elementId].Item2, this.elements[elementId].Item3);
            this.values.Add(Name, Value);
        }

        private void EditorChanged(object sender, EventArgs e)
        {
            string name = null;
            if (sender is Control)
            {
                name = ((Control)sender).Name.Substring(0, ((Control)sender).Name.IndexOf("_"));
            }
            if (sender is TextBox)
            {
                if (((TextBox)sender).UseSystemPasswordChar == false)
                {
                    this.elements.Where(x => x.Item1 == name).FirstOrDefault().Item2.Text = ((TextBox)sender).Text;
                }
            }
            if (sender is NumericUpDown)
            {
                this.elements.Where(x => x.Item1 == name).FirstOrDefault().Item2.Text = ((NumericUpDown)sender).Value.ToString();
            }
            if (sender is ComboBox)
            {
                this.elements.Where(x => x.Item1 == name).FirstOrDefault().Item2.Text = ((ComboBox)sender).Text;
            }
            if (sender is DateTimePicker)
            {
                this.elements.Where(x => x.Item1 == name).FirstOrDefault().Item2.Text = ((DateTimePicker)sender).Value.ToString(((DateTimePicker)sender).CustomFormat);
            }
        }

        private void DynamicTableLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DropdownChanged?.Invoke(this, new GenericEventArgs<string>(((Control)sender).Name.Substring(0, ((Control)sender).Name.IndexOf("_"))));
        }

        public object GetValue(string Name)
        {
            return this.GetValue<object>(Name);
        }

        public T GetValue<T>(string Name)
        {
            if (this.elements.Where(x => x.Item1 == Name).Count() == 0) { throw new ArgumentException(Name + " does not exist"); }
            Control sender = this.elements.Where(x => x.Item1 == Name).FirstOrDefault().Item3;
            if (sender is TextBox)
            {
                return (T)Convert.ChangeType(sender.Text, typeof(T));
            }
            if (sender is NumericUpDown)
            {
                return (T)Convert.ChangeType(((NumericUpDown)sender).Value, typeof(T));
            }
            if (sender is ComboBox)
            {
                return (T)Convert.ChangeType(((ComboBox)sender).SelectedIndex, typeof(T));
            }
            if (sender is DateTimePicker)
            {
                return (T)Convert.ChangeType(((DateTimePicker)sender).Value, typeof(T));
            }
            return default(T);
        }

        public void SetValue(string Name, object Value)
        {
            if (this.elements.Where(x => x.Item1 == Name).Count() == 0) { throw new ArgumentException(Name + " does not exist"); }
            if (Value == null) { Value = ""; }
            Control sender = this.elements.Where(x => x.Item1 == Name).FirstOrDefault().Item3;
            if (sender is TextBox)
            {
                sender.Text = Value.ToString();
            }
            if (sender is NumericUpDown)
            {
                decimal dValue = 0;
                decimal.TryParse(Value.ToString(), out dValue);
                if (dValue < ((NumericUpDown)sender).Minimum) { dValue = ((NumericUpDown)sender).Minimum; }
                if (dValue > ((NumericUpDown)sender).Maximum) { dValue = ((NumericUpDown)sender).Maximum; }
                ((NumericUpDown)sender).Value = dValue;
            }
            if (sender is ComboBox)
            {
                int iValue = 0;
                int.TryParse(Value.ToString(), out iValue);
                if (iValue < 0) { iValue = -1; }
                if (iValue > ((ComboBox)sender).Items.Count - 1) { iValue = -1; }
                ((ComboBox)sender).SelectedIndex = iValue;
            }
        }

        public void Reset()
        {
            foreach (var item in this.elements)
            {
                if (values.ContainsKey(item.Item1))
                {
                    this.SetValue(item.Item1, values[item.Item1]);
                }
            }
        }

        public int Count
        {
            get
            {
                return elements.Count;
            }
        }

        public void RemoveControl(string Name)
        {
            if (this.elements.Where(x => x.Item1 == Name).Count() == 0) { throw new ArgumentException(Name + " does not exist"); }
            if (this.values.ContainsKey(Name)) { this.values.Remove(Name); }
            int row = this.GetRow(this.Controls[Name]);
            int rowHeight = this.Controls[Name].Height;
            for (int i = 0; i < this.Controls[Name].Controls.Count; i++)
            {
                this.Controls[Name].Controls[i].Dispose();
                this.Controls[Name].Controls.RemoveAt(i);
            }
            this.Controls.Remove(this.GetControlFromPosition(0, row));
            this.Controls.Remove(this.GetControlFromPosition(1, row));
            this.elements.RemoveAll(x => x.Item1 == Name);
            this.RowStyles.RemoveAt(row);
            this.MaximumSize = new Size(this.MaximumSize.Width, this.MaximumSize.Height - (int)rowHeight);
            this.MinimumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height - (int)rowHeight);
            this.Size = new Size(this.Size.Width, this.Size.Height - (int)rowHeight);
            if (this.FindForm() != null)
            {
                if (this.FindForm().MaximumSize.Height > 0)
                {
                    this.FindForm().MaximumSize = new Size(this.FindForm().MaximumSize.Width, this.FindForm().MaximumSize.Height - (int)rowHeight);
                }
                if (this.FindForm().MinimumSize.Height > 0)
                {
                    this.FindForm().MinimumSize = new Size(this.FindForm().MinimumSize.Width, this.FindForm().MinimumSize.Height - (int)rowHeight);
                }
                this.FindForm().Size = new Size(this.FindForm().Size.Width, this.FindForm().Size.Height - (int)rowHeight);
            }
        }
    }
}
