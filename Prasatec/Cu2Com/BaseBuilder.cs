using Prasatec.Experience;
using Prasatec.Raden;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Prasatec.Cu2Com
{
    public abstract class BaseBuilder<M, C, B, T> : IControllerBuilder<M, C, B>
        where M : IModel
        where C : ICollection<M>
        where T : IController<M, C, B>
        where B : BaseBuilder<M, C, B, T>
    {
        private static readonly string[] const_validationCallingMethods = new string[] { "create", "duplicate", "update" };

        protected T providingController { get; private set; }
        protected string callingMethod { get; private set; }
        protected bool requiresValidation { get; private set; }
        protected virtual string[] requiresValidationCallingMethods { get { return const_validationCallingMethods;  } }
        protected ulong vID { get; set; }

        public bool IsLocked { get; private set; }

        protected BaseBuilder(T providingController, string callingMethod)
        {
            this.providingController = providingController;
            this.callingMethod = callingMethod.ToLower();
            if (this.requiresValidationCallingMethods.Contains(this.callingMethod))
            {
                this.requiresValidation = true;
            }
        }

        protected abstract void GetValuesFromModel(M Source);
        protected abstract M CreateModelFromValues();
        protected abstract string[] ValidateValues();
        protected abstract IQueryBuilder<M> CreateQueryBuilder(M Source, QueryComparisonOperators Operator, Boolean IdOnly);
        protected abstract IQueryResult<M> CreateErrorResult(string Message);
        protected abstract IQueryResult<M> CreateQueryResult(M Source);

        public IQueryResult<M> Execute()
        {
            M Source = default(M);
            IQueryResult<M> result = null;

            try
            {
                Source = this.Build();
            }
            catch (Exception ex)
            {
                return this.CreateErrorResult(ex.Message);
            }

            switch (callingMethod)
            {
                case "create":
                    result = providingController.Connection.Create<M>(Source);
                    break;
                case "update":
                    result = providingController.Connection.Update<M>(
                        this.CreateQueryBuilder(Source, QueryComparisonOperators.EqualTo, true).Build(), 
                    Source);
                    break;
                case "delete":
                    result = providingController.Connection.Delete<M>(
                        this.CreateQueryBuilder(Source, QueryComparisonOperators.EqualTo, false).Build());
                    break;
                case "count":
                    result = providingController.Connection.Count<M>(
                        this.CreateQueryBuilder(Source, QueryComparisonOperators.EqualTo, false).Build());
                    break;
                case "retrieve":
                    result = providingController.Connection.Retrieve<M>(
                        this.CreateQueryBuilder(Source, QueryComparisonOperators.EqualTo, false).Build());
                    break;
                case "find":
                    result = providingController.Connection.List<M>(
                        this.CreateQueryBuilder(Source, QueryComparisonOperators.IsLike, false).Build());
                    break;
                default:
                    result = this.CreateQueryResult(Source);
                    break;
            }
            if (result == null)
            {
                throw new NotImplementedException(callingMethod + " not implemeneted for execution");
            }
            return result;
        }

        public C GetCollection()
        {
            M Source = default(M);
            try
            {
                Source = this.Build();
            }
            catch
            {
                return default(C);
            }

            return (C)Activator.CreateInstance(typeof(C),
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new object[] {
                    providingController.Connection,
                    this.CreateQueryBuilder(Source, QueryComparisonOperators.EqualTo, true).Build()
                },
                null);
        }

        public B Clone(M Source)
        {
            if (!this.IsLocked)
            {
                this.GetValuesFromModel(Source);
            }
            return (B)this;
        }

        public M Build()
        {
            if (this.requiresValidation)
            {
                string[] errorMessages = this.ValidateValues();
                if (errorMessages != null && errorMessages.Length > 0)
                {
                    throw new ModelValidationFailedException(errorMessages);
                }
                errorMessages = null;
            }
            return CreateModelFromValues();
        }

        public void Lock()
        {
            this.IsLocked = true;
        }

        public B ID(ulong Value)
        {
            if (!this.IsLocked)
            {
                this.vID = Value;
            }
            return (B)this;
        }
        public B ID(M Value)
        {
            if (!this.IsLocked)
            {
                this.vID = Value.ID;
            }
            return (B)this;
        }
        public B ID(ForeignKey<M> Value)
        {
            if (!this.IsLocked)
            {
                this.vID = Value;
            }
            return (B)this;
        }
    }
}
