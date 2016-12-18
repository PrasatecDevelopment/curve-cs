using Prasatec.Cu2Com.Experience;
using Prasatec.Cu2Com.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prasatec.Raden;

namespace Prasatec.Cu2Com
{
    public class UserBuilder : BaseBuilder<UserModel, UserCollection, UserBuilder, UserController>
    {
        public UserBuilder(UserController providingController, string callingMethod) : base(providingController, callingMethod)
        {
        }

        protected override IQueryResult<UserModel> CreateErrorResult(string Message)
        {
            return new QueryResult<UserModel>()
            {
                b_Successful = false,
                s_ErrorMessage = Message,
                i_AffectedRows = 0,
                i_CountPages = 0,
                i_CountRecords = 0,
                i_CountTotal = 0,
                i_InsertId = 0,
                o_Records = null,
                o_ScalarValue = null
            };
        }

        protected override UserModel CreateModelFromValues()
        {
            return new Raden.UserModel()
            {
                ID = vID,
                Login = vLogin,
                Email = vEmail,
                Name = vName,
                CreatedMethod = vCreatedMethod,
                Pin = vPin,
                Manager = vManager,
                Role = vRole,
            };
        }

        protected override IQueryBuilder<UserModel> CreateQueryBuilder(UserModel Source, QueryComparisonOperators Operator, bool IdOnly)
        {
            var queryBuilder = new QueryBuilder<UserModel>();
            if (Source.IsChanged(x => x.ID))
            {
                queryBuilder.Where(x => x.ID, Source.ID, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Login))
            {
                queryBuilder.Where(x => x.Login, Source.Login, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Email))
            {
                queryBuilder.Where(x => x.Email, Source.Email, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Name))
            {
                queryBuilder.Where(x => x.Name, Source.Name, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.CreatedMethod))
            {
                queryBuilder.Where(x => x.CreatedMethod, Source.CreatedMethod, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Pin))
            {
                queryBuilder.Where(x => x.Pin, Source.Pin, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Manager))
            {
                queryBuilder.Where(x => x.Manager, Source.Manager, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Role))
            {
                queryBuilder.Where(x => x.Role, Source.Role, Operator);
            }
            return queryBuilder;
        }

        protected override IQueryResult<UserModel> CreateQueryResult(UserModel Source)
        {
            return null;
        }

        protected override void GetValuesFromModel(UserModel Source)
        {
            this.vID = Source.ID;
            this.vLogin = Source.Login;
            this.vEmail = Source.Email.ToLower();
            this.vName = Source.Name.ToLower();
            this.vCreatedMethod = Source.CreatedMethod;
            this.vPin = Source.Pin;
            this.vManager = Source.Manager;
            this.vRole = Source.Role;
        }

        protected override string[] ValidateValues()
        {
            List<string> errorMessages = new List<string>();
            if (vLogin == null || vLogin.Trim().Length == 0)
            {
                errorMessages.Add("You must provide a login for this user.");
            }
            else if (providingController.Connection.Count<UserModel>(new QueryBuilder<UserModel>().Where(x => x.Login, vLogin).Where(x => x.ID, vID, QueryComparisonOperators.NotEqualTo).Build()).CountTotal > 0)
            {
                errorMessages.Add("User login '" + vLogin + "' already exists.");
            }
            if (vName == null || vName.Trim().Length == 0)
            {
                errorMessages.Add("You must provide a name for this user.");
            }
            string[] result = errorMessages.ToArray();
            errorMessages.Clear(); errorMessages = null;
            return result;
        }

        private string vLogin, vEmail, vName, vCreatedMethod;
        private ushort vPin;
        private ForeignKey<RoleModel> vRole;
        private ForeignKey<UserModel> vManager;

        public UserBuilder Login(string Value)
        {
            if (!this.IsLocked)
            {
                this.vLogin = Value.ToLower();
            }
            return this;
        }
        public UserBuilder Email(string Value)
        {
            if (!this.IsLocked)
            {
                this.vEmail = Value.ToLower();
            }
            return this;
        }
        public UserBuilder Description(string Value)
        {
            if (!this.IsLocked)
            {
                this.vEmail = Value;
            }
            return this;
        }
        public UserBuilder Name(string Value)
        {
            if (!this.IsLocked)
            {
                this.vName = Value;
            }
            return this;
        }
        public UserBuilder CreateMethod(string Value)
        {
            if (!this.IsLocked)
            {
                this.vCreatedMethod = Value;
            }
            return this;
        }
        public UserBuilder Pin(ushort Value)
        {
            if (!this.IsLocked)
            {
                this.vPin = Value;
            }
            return this;
        }
        public UserBuilder Role(ulong Value)
        {
            if (!this.IsLocked)
            {
                this.vRole = Value;
            }
            return this;
        }
        public UserBuilder Role(RoleModel Value)
        {
            if (!this.IsLocked)
            {
                this.vRole = Value.ID;
            }
            return this;
        }
        public UserBuilder Role(ForeignKey<RoleModel> Value)
        {
            if (!this.IsLocked)
            {
                this.vRole = Value;
            }
            return this;
        }
        public UserBuilder Manager(ulong Value)
        {
            if (!this.IsLocked)
            {
                this.vManager = Value;
            }
            return this;
        }
        public UserBuilder Manager(UserModel Value)
        {
            if (!this.IsLocked)
            {
                this.vManager = Value.ID;
            }
            return this;
        }
        public UserBuilder Manager(ForeignKey<UserModel> Value)
        {
            if (!this.IsLocked)
            {
                this.vManager = Value;
            }
            return this;
        }
    }
}
