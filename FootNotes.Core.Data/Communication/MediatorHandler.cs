using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using MediatR;
using FootNotes.Core.Data.EventSourcing;

namespace FootNotes.Core.Data.Communication
{
    public class MediatorHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository) : IMediatorHandler
    {
        public async Task<MessageResponse> SendCommand<T>(T command) where T : Command
        {
            return await mediator.Send(command);            
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await mediator.Publish(@event);
            await eventSourcingRepository.SaveEvent(@event);
        }
    }
}
