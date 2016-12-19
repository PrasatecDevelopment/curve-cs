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
    public sealed class EventBuilder : BaseBuilder<EventModel, EventCollection, EventBuilder, EventController>
    {
        public EventBuilder(EventController providingController, string callingMethod) : base(providingController, callingMethod)
        {
        }
        
        private string vCode, vContent;
        private EventTypes vType;

        protected override IQueryResult<EventModel> CreateErrorResult(string Message)
        {
            return new QueryResult<EventModel>()
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

        protected override EventModel CreateModelFromValues()
        {
            return new Raden.EventModel()
            {
                ID = vID,
                Code = vCode,
                Content = vContent,
                Type = vType
            };
        }

        protected override IQueryBuilder<EventModel> CreateQueryBuilder(EventModel Source, QueryComparisonOperators Operator, bool IdOnly)
        {
            var queryBuilder = new QueryBuilder<EventModel>();
            if (Source.IsChanged(x => x.ID))
            {
                queryBuilder.Where(x => x.ID, Source.ID, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Code))
            {
                queryBuilder.Where(x => x.Code, Source.Code, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Content))
            {
                queryBuilder.Where(x => x.Content, Source.Content, Operator);
            }
            if (!IdOnly && Source.IsChanged(x => x.Type))
            {
                queryBuilder.Where(x => x.Type, Source.Type, Operator);
            }
            return queryBuilder;
        }

        protected override IQueryResult<EventModel> CreateQueryResult(EventModel Source)
        {
            return null;
        }

        protected override void GetValuesFromModel(EventModel Source)
        {
            this.vID = Source.ID;
            this.vCode = Source.Code;
            this.vContent = Source.Content;
            this.vType = Source.Type;
        }

        protected override string[] ValidateValues()
        {
            List<string> errorMessages = new List<string>();
            if (vType == EventTypes.NotSpecified)
            {
                errorMessages.Add("You must provide an event type.");
            }
            if (vContent == null || vContent.Trim().Length == 0)
            {
                errorMessages.Add("You must type some content.");
            }
            string[] result = errorMessages.ToArray();
            errorMessages.Clear(); errorMessages = null;
            return result;
        }

        public EventBuilder Code(string Value)
        {
            if (!this.IsLocked)
            {
                this.vCode = Value;
            }
            return this;
        }
        public EventBuilder Content(string Value)
        {
            if (!this.IsLocked)
            {
                this.vContent = Value;
            }
            return this;
        }
        public EventBuilder Type(EventTypes Value)
        {
            if (!this.IsLocked)
            {
                this.vType = Value;
            }
            return this;
        }
    }
}
