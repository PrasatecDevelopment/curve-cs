namespace Prasatec.Experience.DynamicWindows
{
    partial class EditorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pFields = new Prasatec.Experience.Elements.DynamicTableLayout();
            this.pButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.bClose = new System.Windows.Forms.Button();
            this.bEdit = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.pButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pFields
            // 
            this.pFields.BackColor = System.Drawing.Color.Transparent;
            this.pFields.ColumnCount = 2;
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFields.Location = new System.Drawing.Point(8, 8);
            this.pFields.MaximumSize = new System.Drawing.Size(10000, 6);
            this.pFields.MinimumSize = new System.Drawing.Size(0, 6);
            this.pFields.Mode = Prasatec.Experience.DynamicEditorModes.View;
            this.pFields.Name = "pFields";
            this.pFields.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.pFields.RowCount = 2;
            this.pFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pFields.Size = new System.Drawing.Size(418, 6);
            this.pFields.TabIndex = 0;
            // 
            // pButtons
            // 
            this.pButtons.Controls.Add(this.bClose);
            this.pButtons.Controls.Add(this.bEdit);
            this.pButtons.Controls.Add(this.bCancel);
            this.pButtons.Controls.Add(this.bSave);
            this.pButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pButtons.Location = new System.Drawing.Point(8, 14);
            this.pButtons.Name = "pButtons";
            this.pButtons.Size = new System.Drawing.Size(418, 32);
            this.pButtons.TabIndex = 1;
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(318, 0);
            this.bClose.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(100, 32);
            this.bClose.TabIndex = 0;
            this.bClose.Text = "&Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bEdit
            // 
            this.bEdit.Location = new System.Drawing.Point(212, 0);
            this.bEdit.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.bEdit.Name = "bEdit";
            this.bEdit.Size = new System.Drawing.Size(100, 32);
            this.bEdit.TabIndex = 1;
            this.bEdit.Text = "&Edit";
            this.bEdit.UseVisualStyleBackColor = true;
            this.bEdit.Click += new System.EventHandler(this.bEdit_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(106, 0);
            this.bCancel.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(100, 32);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(0, 0);
            this.bSave.Margin = new System.Windows.Forms.Padding(0);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(100, 32);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "&Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 54);
            this.Controls.Add(this.pButtons);
            this.Controls.Add(this.pFields);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditorWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditorWindow";
            this.pButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Elements.DynamicTableLayout pFields;
        private System.Windows.Forms.FlowLayoutPanel pButtons;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bEdit;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bSave;
    }
}