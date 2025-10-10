using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FootNotes.Core.Messages
{
    public abstract class Event : Message, INotification
    {        
        public EventCRUDType EventType { get; private set; }
        protected Event(EventCRUDType eventType)
        {            
            EventType = eventType;
        }

        protected Event(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        protected Event(Guid aggregateId, EventCRUDType eventType)
        {
            AggregateId = aggregateId;
            EventType = eventType;
        }

    }

    public enum EventCRUDType
    {
        Other,
        Created,
        Updated,
        Deleted
    }
}
