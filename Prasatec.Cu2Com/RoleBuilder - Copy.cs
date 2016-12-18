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
    public sealed class RoleBuilder : IControllerBuilder<RoleModel, RoleCollection, RoleBuilder>
    {
        internal RoleBuilder(RoleController providingController)
        {
            this.providingController = providingController;
            StackTrace trace = new StackTrace();
            this.callingMethod = trace.GetFrame(1).GetMethod().Name.ToLower();
            if (this.callingMethod == "create" || this.callingMethod == "duplicate" || this.callingMethod == "update")
            {
                this.requiresValidation = true;
            }
        }

        private RoleController providingController;
        private string callingMethod;
        private bool requiresValidation;

        private ulong vID;
        private string vName;
        private string vDescription;
        private UserModes vMode;

        public bool IsLocked { get; private set; }

        public RoleBuilder Clone(RoleModel Source)
        {
            if (!this.IsLocked)
            {
                this.vID = Source.ID;
                this.vName = Source.Name;
                this.vDescription = Source.Description;
                this.vMode = Source.Mode;
            }
            return this;
        }

        public IQueryResult<RoleModel> Execute()
        {
            IQueryResult<RoleModel> result = null;
            RoleModel model = null;
            try
            {
                model = this.Build();
            }
            catch (Exception ex)
            {
                result = new QueryResult<RoleModel>()
                {
                    b_Successful = false,
                    s_ErrorMessage = ex.Message,
                    i_AffectedRows = 0,
                    i_CountPages = 0,
                    i_CountRecords = 0,
                    i_CountTotal = 0,
                    i_InsertId = 0,
                    o_Records = null,
                    o_ScalarValue = null
                };
                return result;
            }
            switch (callingMethod)
            {
                case "create":
                    result = providingController.Connection.Create<RoleModel>(model);
                    break;
                case "retrieve":
                    var rq = new QueryBuilder<RoleModel>();
                    if (model.IsChanged(x => x.ID))
                    {
                        rq.Where(x => x.ID, model.ID);
                    }
                    if (model.IsChanged(x => x.Name))
                    {
                        rq.Where(x => x.Name, model.Name);
                    }
                    if (model.IsChanged(x => x.Description))
                    {
                        rq.Where(x => x.Description, model.Description);
                    }
                    if (model.IsChanged(x => x.Mode))
                    {
                        rq.Where(x => x.Mode, model.Mode);
                    }
                    result = providingController.Connection.Retrieve<RoleModel>(rq.Build());
                    rq = null;
                    break;
                case "count":
                    var cq = new QueryBuilder<RoleModel>();
                    if (model.IsChanged(x => x.ID))
                    {
                        cq.Where(x => x.ID, model.ID);
                    }
                    if (model.IsChanged(x => x.Name))
                    {
                        cq.Where(x => x.Name, model.Name);
                    }
                    if (model.IsChanged(x => x.Description))
                    {
                        cq.Where(x => x.Description, model.Description);
                    }
                    if (model.IsChanged(x => x.Mode))
                    {
                        cq.Where(x => x.Mode, model.Mode);
                    }
                    result = providingController.Connection.Count<RoleModel>(cq.Build());
                    cq = null;
                    break;
                case "find":
                    var fq = new QueryBuilder<RoleModel>();
                    if (model.IsChanged(x => x.ID))
                    {
                        fq.Where(x => x.ID, model.ID, QueryComparisonOperators.IsLike);
                    }
                    if (model.IsChanged(x => x.Name))
                    {
                        fq.Where(x => x.Name, model.Name, QueryComparisonOperators.IsLike);
                    }
                    if (model.IsChanged(x => x.Description))
                    {
                        fq.Where(x => x.Description, model.Description, QueryComparisonOperators.IsLike);
                    }
                    if (model.IsChanged(x => x.Mode))
                    {
                        fq.Where(x => x.Mode, model.Mode, QueryComparisonOperators.IsLike);
                    }
                    result = providingController.Connection.Retrieve<RoleModel>(fq.Build());
                    rq = null;
                    break;
                case "update":
                    var uq = new QueryBuilder<RoleModel>().Where(x => x.ID, model.ID).Build();
                    result = providingController.Connection.Update<RoleModel>(uq, model);
                    uq = null;
                    break;
                case "delete":
                    var ud = new QueryBuilder<RoleModel>().Where(x => x.ID, model.ID).Build();
                    result = providingController.Connection.Delete<RoleModel>(ud);
                    ud = null;
                    break;
            }
            if (result == null)
            {
                throw new NotImplementedException(callingMethod + " not implemented for execution.");
            }
            return result;
        }
        public RoleCollection GetCollection()
        {
            RoleModel model = null;
            try
            {
                model = this.Build();
            }
            catch //(Exception ex)
            {
                return null;
            }
            var rq = new QueryBuilder<RoleModel>();
            if (model.IsChanged(x => x.ID))
            {
                rq.Where(x => x.ID, model.ID);
            }
            if (model.IsChanged(x => x.Name))
            {
                rq.Where(x => x.Name, model.Name);
            }
            if (model.IsChanged(x => x.Description))
            {
                rq.Where(x => x.Description, model.Description);
            }
            if (model.IsChanged(x => x.Mode))
            {
                rq.Where(x => x.Mode, model.Mode);
            }
            RoleCollection result = new Raden.RoleCollection(providingController.Connection, rq.Build());
            //result.
            return result;
        }

        public RoleModel Build()
        {
            if (this.requiresValidation)
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
                if (errorMessages.Count > 0)
                {
                    throw new ModelValidationFailedException(errorMessages.ToArray());
                }
                errorMessages = null;
            }
            return new Raden.RoleModel()
            {
                ID = vID,
                Name = vName,
                Description = vDescription,
                Mode = vMode
            };
        }

        public void Lock()
        {
            this.IsLocked = true;
        }

        public RoleBuilder ID(ulong Value)
        {
            if (!this.IsLocked)
            {
                this.vID = Value;
            }
            return this;
        }
        public RoleBuilder ID(RoleModel Value)
        {
            if (!this.IsLocked)
            {
                this.vID = Value.ID;
            }
            return this;
        }
        public RoleBuilder ID(ForeignKey<RoleModel> Value)
        {
            if (!this.IsLocked)
            {
                this.vID = Value;
            }
            return this;
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
