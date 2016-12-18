using Prasatec.Experience;
using System.Linq;
using Prasatec.Cu2Com.Raden;
using Prasatec.Raden;
using Prasatec.Experience.DynamicWindows;
using System;

namespace Prasatec.Cu2Com.Experience
{
    public class UserController : BaseController<UserModel, UserCollection, UserBuilder>
    {
        private RoleModel[] rc;
        private UserModel[] mc;

        public UserController(IConnection Connection) : base(Connection)
        {
        }

        protected override string TitleCreate { get { return "Create New User"; } }
        protected override string TitleEdit { get { return "Edit User #"; } }
        protected override string TitleView { get { return "View User #"; } }

        protected override IWindowCollection CreateCollection()
        {
            RoleModel[] roles = Connection.List<RoleModel>(
                new QueryBuilder<RoleModel>()
                .Where(x => x.Mode, (ushort)UserModes.Disabled, QueryComparisonOperators.GreaterOrEqual)
                .Build()).Records;

            UserModel[] managers = Connection.List<UserModel>(
                new QueryBuilder<UserModel>()
                .Where<RoleModel>(x => x.ID, y => y.Role)
                .Where<RoleModel>(x => x.Mode, (ushort)UserModes.Manager, QueryComparisonOperators.GreaterOrEqual)
                .Build()).Records;

            if (roles == null) { roles = new RoleModel[0]; }
            if (managers == null) { managers = new UserModel[0]; }

            if (roles.Length == 0)
            {
                throw new Exception("No roles exist. You must create a role before you can create a user.");
            }

            this.rc = roles;

            this.mc = new UserModel[managers.Length + 1];
            this.mc[0] = new UserModel() { Name = "No Manager" };
            Array.Copy(managers, 0, this.mc, 1, managers.Length);
            return new CollectionWindow() { Text = "User Collection" };
        }

        protected override IWindowEditor CreateEditor()
        {
            var result = new EditorWindow();

            result.Fields.AddTextbox("vLogin", "User Login", editorModel.Name);
            result.Fields.AddTextbox("vEmail", "Email Address", editorModel.Name);
            result.Fields.AddTextbox("vName", "Display Name", editorModel.Name);
            result.Fields.AddNumber("vPin", "Login PIN", 1000, 9999, 0);
            result.Fields.AddDropdown("vRole", "Role", this.rc, 0);
            result.Fields.AddDropdown("vManager", "Manager", this.mc, 0);

            return result;
        }

        protected override UserModel CreateModel()
        {
            return new Raden.UserModel();
        }

        protected override void ParseRow(UserModel item)
        {
            if (collectionWindow == null) { return; }
            collectionWindow.AddRow(item.Login, item.Name, rc.Where(x => x.ID == item.Role).Select(x => x.Name).FirstOrDefault());
        }

        protected override UserBuilder PrepareBuilder(UserBuilder builder)
        {
            builder = builder
                .Login(editorWindow.Fields.GetValue<string>("vLogin"))
                .Email(editorWindow.Fields.GetValue<string>("vEmail"))
                .Name(editorWindow.Fields.GetValue<string>("vName"))
                .Pin(editorWindow.Fields.GetValue<ushort>("vPin"))
                .Role(editorWindow.Fields.GetValue<int>("vRole") == -1 ? 0 : rc[editorWindow.Fields.GetValue<int>("vRole")].ID)
                .Manager(editorWindow.Fields.GetValue<int>("vManager") == -1 ? 0 : mc[editorWindow.Fields.GetValue<int>("vManager")].ID)
                .CreateMethod("In Collection");
            return builder;
        }
    }
}
