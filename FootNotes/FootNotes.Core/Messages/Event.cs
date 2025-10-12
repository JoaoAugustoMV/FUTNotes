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
        protected Event(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

    }
}
