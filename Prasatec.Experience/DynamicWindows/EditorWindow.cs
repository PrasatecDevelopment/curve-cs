using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Experience.DynamicWindows
{
    public partial class EditorWindow : BaseWindows.BasePaddedWindow, IWindowEditor
    {
        private string s_EditTitle, s_ViewTitle;
        public event EventHandler<GenericEventArgs<bool>> Saved;
        public IElementDynamicTableLayout Fields { get { return pFields; } }
        public string EditTitle
        {
            get { return s_EditTitle; }
            set
            {
                s_EditTitle = value;
                if (this.Mode == DynamicEditorModes.Edit)
                {
                    this.Text = value;
                }
            }
        }
        public string ViewTitle
        {
            get { return s_ViewTitle; }
            set
            {
                s_ViewTitle = value;
                if (this.Mode == DynamicEditorModes.View)
                {
                    this.Text = value;
                }
            }
        }
        public Boolean CanChangeMode
        {
            get { return b_CanChangeMode; }
            set
            {
                b_CanChangeMode = value;
                this.Mode = this.Mode;
            }
        }
        public DynamicEditorModes Mode
        {
            get { return Fields.Mode; }
            set
            {
                Fields.Mode = value;
                if (value == DynamicEditorModes.Edit)
                {
                    this.Text = EditTitle;
                }
                else
                {
                    this.Text = ViewTitle;
                }
                if (CanChangeMode == false)
                {
                    if (value == DynamicEditorModes.Edit)
                    {
                        bSave.Visible = true;
                    }
                    else
                    {
                        bSave.Visible = false;
                    }
                    bCancel.Visible = false;
                    bEdit.Visible = false;
                    bClose.Visible = true;
                }
                else
                {
                    if (value == DynamicEditorModes.Edit)
                    {
                        bSave.Visible = true;
                        bCancel.Visible = true;
                        bEdit.Visible = false;
                        bClose.Visible = false;
                    }
                    else if (value == DynamicEditorModes.View)
                    {
                        bSave.Visible = false;
                        bCancel.Visible = false;
                        bEdit.Visible = true;
                        bClose.Visible = true;
                    }
                }
                bSave.BringToFront();
                bCancel.BringToFront();
                bEdit.BringToFront();
                bClose.BringToFront();
            }
        }

        IWindowEditor IWindowEditor.Base
        {
            get
            {
                return this;
            }
        }

        private Boolean b_CanChangeMode;
        public EditorWindow()
        {
            InitializeComponent();
            this.CanChangeMode = true;
            this.Mode = DynamicEditorModes.View;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            bool successful = false;
            pFields.Enabled = false;
            pButtons.Enabled = false;
            if (this.Saved == null)
            {
                successful = true;
            }
            else
            {
                var ea = new GenericEventArgs<bool>(false);
                this.Saved.Invoke(this, ea);
                successful = ea.Value;
            }
            pFields.Enabled = true;
            pButtons.Enabled = true;
            if (!CanChangeMode)
            {
                this.Close();
            }
            else
            {
                this.Mode = DynamicEditorModes.View;
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Fields.Reset();
            this.Mode = DynamicEditorModes.View;
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            if (CanChangeMode)
            {
                this.Mode = DynamicEditorModes.Edit;
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
