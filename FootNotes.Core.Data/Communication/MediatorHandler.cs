using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Data.EventSourcing;
using FootNotes.Core.Messages;
using MediatR;

namespace FootNotes.Core.Data.Communication
{
    public class MediatorHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository) : IMediatorHandler
    {
        //IRequest<AnnotationSessionViewModel>
        public async Task<TResponse> Query<TResponse>(Query<TResponse> query)
        {
            return await mediator.Send(query);
        }

        public async Task<CommandResponse> SendCommand<T>(T command) where T : Command
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
