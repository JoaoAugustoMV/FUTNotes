using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Application.Commands.AnnotationSessionCommands;
using FootNotes.Annotations.Application.Events.AnnotationSessionEvents;
using FootNotes.Annotations.Domain.AnnotationSessionModels;
using FootNotes.Annotations.Domain.Repositories;
using FootNotes.Core.Application;
using FootNotes.Core.Messages;
using MediatR;

namespace FootNotes.Annotations.Application.CommandHandlers
{
    public class AnnotationSessionCommandHandler(IAnnotationSessionRepository annotationSessionRepository) : 
        IRequestHandler<CreateNewAnnotationSessionCommand, CommandResponse>,
        IRequestHandler<AddAnnotationCommand, CommandResponse>
    {
        public async Task<CommandResponse> Handle(CreateNewAnnotationSessionCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid(out string error))
            {
                return new CommandResponse(Guid.Empty, false, error);
            }

            try
            {
                AnnotationSession annotationSession = AnnotationSession.CreateNew(
                    request.UserId,
                    request.MatchId,
                    request.SessionType
                );

                annotationSession.AddEvent(
                    new CreateNewAnnotationSessionEvent(
                    annotationSession.Id,
                    annotationSession.UserId,
                    annotationSession.MatchId,
                    annotationSession.Type,
                    annotationSession.Started
                ));

                await annotationSessionRepository.AddAsync(annotationSession);

                return new CommandResponse(annotationSession.Id, true);
            }
            catch (Exception ex)
            {
                return new CommandResponse(Guid.Empty, false, ex.Message);                
            }            
        }

        public async Task<CommandResponse> Handle(AddAnnotationCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid(out string error))
            {
                return new CommandResponse(Guid.Empty, false, error);
            }

            AnnotationSession? session = await annotationSessionRepository.GetByIdAsync(command.AnnotationSessionId);
            if (session == null)
            {
                return new CommandResponse(Guid.Empty, false, "The provided AnnotationSessionId does not exist.");                
            }            

            session.AddAnnotation(command.Description, command.Minute, command.Type);

            session.AddEvent(new AddAnnotationEvent(session.Id, command.Description, command.Minute, command.Type));

            await annotationSessionRepository.AddRangeAsync(session.Annotations);

            return new CommandResponse(session.Id, true);
        }
    }
}
