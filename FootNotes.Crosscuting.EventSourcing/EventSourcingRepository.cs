using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FootNotes.Core.Data.EventSourcing;
using FootNotes.Core.Messages;
using KurrentDB.Client;
using static KurrentDB.Client.KurrentDBClient;
using EventData = KurrentDB.Client.EventData;

namespace FootNotes.Crosscuting.EventSourcing
{
    public class EventSourcingRepository(IEventSourcingService eventSourcingService) : IEventSourcingRepository
    {
        public async Task SaveEvent<TEvent>(TEvent newEvent) where TEvent : Event
        {
            await eventSourcingService.GetClient()
                .AppendToStreamAsync(
                    newEvent.AggregateId.ToString(),
                    StreamState.Any,
                    FormatEvent(newEvent)
            );
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
        {
            ReadStreamResult? streamResult = eventSourcingService.GetClient()
                .ReadStreamAsync(Direction.Forwards, aggregateId.ToString(), StreamPosition.Start, 500);

            List<ResolvedEvent> events = await streamResult.ToListAsync();


            List<StoredEvent> eventList = [];

            foreach (ResolvedEvent resolvedEvent in events)
            {                
                string? dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
                EventRecord? jsonData = JsonSerializer.Deserialize<EventRecord>(dataEncoded);

                StoredEvent storedEvent = new (
                    resolvedEvent.Event.EventId.ToGuid(),
                    resolvedEvent.Event.EventType,
                    jsonData!.Created,
                    dataEncoded);

                eventList.Add(storedEvent);
            }

            return eventList.OrderBy(e => e.Timestamp);
        }


        private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent newEvent) where TEvent : Event
        {            
            yield return new EventData(
                Uuid.NewUuid(),
                newEvent.MessageType,                
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newEvent, newEvent.GetType())),
                null);
        }
    }
}
