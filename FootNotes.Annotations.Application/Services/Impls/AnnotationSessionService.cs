using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Application.Commands.AnnotationSessionCommands;
using FootNotes.Annotations.Application.Requests;
using FootNotes.Annotations.Domain.Repositories;
using FootNotes.Core.Application;
using FootNotes.Core.Data.Communication;
using FootNotes.Core.Messages;
using Microsoft.Extensions.Logging;

namespace FootNotes.Annotations.Application.Services.Impls
{
    public class AnnotationSessionService(
        IMediatorHandler mediatorHandler,
        IAnnotationMatchService annotationMatchService,
        IAnnotationSessionRepository annotationSessionRepository,
        ILogger<AnnotationSessionService> logger) : IAnnotationSessionService
    {
        public async Task<Result<Guid>> AddAnnotationAsync(AddAnnotationRequest request)
        {
            if (!request.IsValid(out var msg))
            {
                return (Result<Guid>.Failure(msg));
            }

            AddAnnotationCommand command = new()
            {
                AnnotationSessionId = request.AnnotationSessionId,
                Description = request.Description,
                Minute = request.Minute,
                Type = request.Type,
            };

            CommandResponse response = await mediatorHandler.SendCommand(command);

            if (response.Sucess)
            {
                return Result<Guid>.Success(response.AggregateId);
            }
            
            return Result<Guid>.Failure(response.Message ?? "Unknown error occurred while adding annotation.");
                       
        }

        public async Task<Result<Guid>> CreateNewAnnotationSessionAsync(CreateNewAnnotationSessionRequest request)
        {
            if (!request.IsValid(out var msg))
            {
                return Result<Guid>.Failure(msg);
                
            }

            if (!await annotationMatchService.ExistsMatchId(request.MatchId))
            {
                return Result<Guid>.Failure("The provided MatchId does not exist.");
            }

            CreateNewAnnotationSessionCommand command = new()
            {
                UserId = request.UserId,
                MatchId = request.MatchId,
                SessionType = request.SessionType
            };

            CommandResponse response = await mediatorHandler.SendCommand(command);

            if (response.Sucess)
            {
                return Result<Guid>.Success(response.AggregateId);
            }
            else
            {
                return Result<Guid>.Failure(response.Message ?? "Unknown error occurred while creating annotation session.");
            }
            
        }
    }
}
