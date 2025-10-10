using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;

namespace FootNotes.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SaveEvent<TEvent>(TEvent newEvent) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}
