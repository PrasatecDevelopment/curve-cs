using Prasatec.Experience;
using System;
using Prasatec.Cu2Com.Raden;
using Prasatec.Raden;
using Prasatec.Experience.DynamicWindows;

namespace Prasatec.Cu2Com.Experience
{
    public class DelegationController : BaseController<DelegationModel, DelegationCollection, DelegationBuilder>
    {
        public DelegationController(IConnection Connection) : base(Connection)
        {
        }

        protected override string TitleCreate { get { return "Create New Delegation"; } }
        protected override string TitleEdit { get { return "Edit Delegation #"; } }
        protected override string TitleView { get { return "View Delegation #"; } }

        protected override IWindowCollection CreateCollection()
        {
            return new CollectionWindow() { Text = "Delegation Collection" };
        }

        protected override IWindowEditor CreateEditor()
        {
            var result = new EditorWindow();
            string[] modenames = Enum.GetNames(typeof(UserModes));
            /*int modeindex = Array.IndexOf(modenames, Enum.GetName(typeof(UserModes), editorModel.Mode));

            result.Fields.AddTextbox("vName", "Delegation Name", editorModel.Name);
            result.Fields.AddContent("vDescription", "Description", editorModel.Description);
            result.Fields.AddDropdown("vMode", "User Mode", modenames, modeindex);*/

            return result;
        }

        protected override DelegationModel CreateModel()
        {
            return new Raden.DelegationModel();
        }

        protected override void ParseRow(DelegationModel item)
        {
            if (collectionWindow == null) { return; }
            //collectionWindow.AddRow(item.Name, item.Mode);
        }

        protected override DelegationBuilder PrepareBuilder(DelegationBuilder builder)
        {
            /*builder = builder
                .Name(editorWindow.Fields.GetValue<string>("vName"))
                .Description(editorWindow.Fields.GetValue<string>("vDescription"))
                .Mode((UserModes)Enum.GetValues(typeof(UserModes)).GetValue(editorWindow.Fields.GetValue<int>("vMode")));*/
            return builder;
        }
    }
}
