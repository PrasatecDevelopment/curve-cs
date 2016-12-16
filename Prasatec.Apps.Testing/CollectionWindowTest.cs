using Prasatec.Experience.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Apps.Testing
{
    class CollectionWindowTest : Prasatec.Experience.DynamicWindows.CollectionWindow
    {

        internal CollectionWindowTest() : base()
        {
            /*Categories.AddTextbox("textbox", "Textbox", "blah");
            Categories.AddPassword("password", "Password", "blah");
            Categories.AddNumber("number", "Number", 100, 200, 50);
            Categories.AddContent("content", "Content", "blah");
            Categories.AddDropdown("dropdown", "Dropdown", new object[] { "Item 1", "item 2", "item 3", "item 4", "item 5", "item 6" }, 5);
            Categories.AddTimestamp("timestamp", "Timestamp", DateTime.Now);*/
            //this.Controller.Categorized = true;
            //return;
            this.Controller.AddCategory("user", "Users", new string[] { "item 1", "item2", "Root" });
            this.Controller.AddCategory("user1", "Users1", new string[] { "item 1", "item2", "Root" });
            this.Controller.AddCategory("user2", "Users2", new string[] { "item 1", "item2", "Root" });
            this.Controller.AddCategory("user3", "Users3", new string[] { "item 1", "item2", "Root" });
            this.Controller.CategoryChanged += Controller_CategoryChanged;
            this.Controller.Columns = new string[] { "Column 1", "Column 2", "Column 3" };
            this.Controller.AddRow("column1", "column2", "column3 blah blah blah");
            Controller.PageCount = 5;
            //this.Controls.Add(new ActionButton(this) { Text = "blah", Dock = System.Windows.Forms.DockStyle.Fill });
        }

        private void Controller_CategoryChanged(object sender, Experience.CollectionCategoryChangedEventArgs e)
        {
            if (e.Name == "user2")
            {
                 Controller.RemoveCategory("user3");
            }
            //throw new NotImplementedException();
        }

        private void B_Click(object sender, EventArgs e)
        {
            //Categories.SetValue("dropdown", 2);
            //return;
            /*if (Categories.Mode == Experience.DynamicEditorModes.View)
            {
                Categories.Mode = Experience.DynamicEditorModes.Edit;
            }
            else
            {
                Categories.Mode = Experience.DynamicEditorModes.View;
            }*/
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WindowTest
            // 
            this.Name = "WindowTest";
            this.ResumeLayout(false);

        }
    }
}
