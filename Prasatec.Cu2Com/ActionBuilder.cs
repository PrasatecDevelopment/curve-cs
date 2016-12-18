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
    public sealed class ActionBuilder : BaseBuilder<ActionModel, ActionCollection, ActionBuilder, ActionController>
    {
        public ActionBuilder(ActionController providingController, string callingMethod) : base(providingController, callingMethod)
        {
        }

        private string vName;
        private string vDescription;
        private UserModes vMode;

        protected override IQueryResult<ActionModel> CreateErrorResult(string Message)
        {
            return new QueryResult<ActionModel>()
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

        protected override ActionModel CreateModelFromValues()
        {
            return new Raden.ActionModel()
            {
                ID = vID,
            };
        }

        protected override IQueryBuilder<ActionModel> CreateQueryBuilder(ActionModel Source, QueryComparisonOperators Operator, bool IdOnly)
        {
            var queryBuilder = new QueryBuilder<ActionModel>();
            if (Source.IsChanged(x => x.ID))
            {
                queryBuilder.Where(x => x.ID, Source.ID, Operator);
            }
            return queryBuilder;
        }

        protected override IQueryResult<ActionModel> CreateQueryResult(ActionModel Source)
        {
            return null;
        }

        protected override void GetValuesFromModel(ActionModel Source)
        {
            this.vID = Source.ID;
        }

        protected override string[] ValidateValues()
        {
            List<string> errorMessages = new List<string>();

            string[] result = errorMessages.ToArray();
            errorMessages.Clear(); errorMessages = null;
            return result;
        }

        public ActionBuilder Name(string Value)
        {
            if (!this.IsLocked)
            {
                this.vName = Value;
            }
            return this;
        }
        public ActionBuilder Description(string Value)
        {
            if (!this.IsLocked)
            {
                this.vDescription = Value;
            }
            return this;
        }
        public ActionBuilder Mode(UserModes Value)
        {
            if (!this.IsLocked)
            {
                this.vMode = Value;
            }
            return this;
        }
        public ActionBuilder Mode(int Value)
        {
            if (!this.IsLocked)
            {
                this.vMode = (UserModes)Value;
            }
            return this;
        }
    }
}
