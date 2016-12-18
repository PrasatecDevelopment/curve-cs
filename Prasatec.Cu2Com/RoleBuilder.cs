using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prasatec.Cu2Com.Raden;
using Prasatec.Raden;
using Prasatec.Cu2Com.Experience;
using System.Diagnostics;

namespace Prasatec.Cu2Com
{
    public sealed class RoleBuilder : BaseBuilder<RoleModel, RoleCollection, RoleBuilder, RoleController>
    {
        public RoleBuilder(RoleController providingController, string callingMethod) : base(providingController, callingMethod)
        {
        }

        private string vName;
        private string vDescription;
        private UserModes vMode;

        protected override IQueryResult<RoleModel> CreateErrorResult(string Message)
        {
            return new QueryResult<RoleModel>()
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

        protected override RoleModel CreateModelFromValues()
        {
            return new Raden.RoleModel()
            {
                ID = vID,
                Name = vName,
                Description = vDescription,
                Mode = vMode
            };
        }

        protected override IQueryBuilder<RoleModel> CreateQueryBuilder(RoleModel Source, QueryComparisonOperators Operator, bool IdOnly)
        {
            var queryBuilder = new QueryBuilder<RoleModel>();
            if (Source.IsChanged(x => x.ID))
            {
                queryBuilder.Where(x => x.ID, Source.ID, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Name))
            {
                queryBuilder.Where(x => x.Name, Source.Name, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Description))
            {
                queryBuilder.Where(x => x.Description, Source.Description, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Mode))
            {
                queryBuilder.Where(x => x.Mode, Source.Mode, Operator);
            }
            return queryBuilder;
        }

        protected override IQueryResult<RoleModel> CreateQueryResult(RoleModel Source)
        {
            return null;
        }

        protected override void GetValuesFromModel(RoleModel Source)
        {
            this.vID = Source.ID;
            this.vName = Source.Name;
            this.vDescription = Source.Description;
            this.vMode = Source.Mode;
        }

        protected override string[] ValidateValues()
        {
            List<string> errorMessages = new List<string>();
            if (vName == null || vName.Trim().Length == 0)
            {
                errorMessages.Add("You must provide a name for this role.");
            }
            else if (providingController.Connection.Count<RoleModel>(new QueryBuilder<RoleModel>().Where(x => x.Name, vName).Where(x => x.ID, vID, QueryComparisonOperators.NotEqualTo).Build()).CountTotal > 0)
            {
                errorMessages.Add("Role '" + vName + "' already exists.");
            }
            if ((int)vMode < 0 || (int)vMode > 65535 || !Enum.GetValues(typeof(UserModes)).Cast<ushort>().Contains((ushort)vMode))
            {
                errorMessages.Add("You must provide a valid user mode for this role.");
            }
            string[] result = errorMessages.ToArray();
            errorMessages.Clear(); errorMessages = null;
            return result;
        }

        public RoleBuilder Name(string Value)
        {
            if (!this.IsLocked)
            {
                this.vName = Value;
            }
            return this;
        }
        public RoleBuilder Description(string Value)
        {
            if (!this.IsLocked)
            {
                this.vDescription = Value;
            }
            return this;
        }
        public RoleBuilder Mode(UserModes Value)
        {
            if (!this.IsLocked)
            {
                this.vMode = Value;
            }
            return this;
        }
        public RoleBuilder Mode(int Value)
        {
            if (!this.IsLocked)
            {
                this.vMode = (UserModes)Value;
            }
            return this;
        }
    }
}
