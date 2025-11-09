using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Application.Commands.AnnotationSessionCommands;
using FootNotes.Annotations.Application.QueryStack.Queries;
using FootNotes.Annotations.Application.QueryStack.ViewModels;
using FootNotes.Annotations.Domain.Repositories;
using FootNotes.Core.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.Annotations.Application.QueryStack.QueriesHandlers
{
    public class AnnotationSessionQueryHandler(IAnnotationSessionRepository annotationSessionRepository) :
        IRequestHandler<GetAnnotationSessionByIdQuery, AnnotationSessionViewModel?>
    {

        public async Task<AnnotationSessionViewModel?> Handle(GetAnnotationSessionByIdQuery request, CancellationToken cancellationToken)
        {
            return await annotationSessionRepository.GetAll().Select(s => new AnnotationSessionViewModel(
                s.UserId,
                s.MatchId,
                s.Started,
                s.Ended,
                s.Status,
                s.Type,
                s.Annotations.Select(a => new AnnotationViewModel
                {
                    TimeStamp = a.TimeStamp,
                    Type = a.Type,
                    Description = a.Description,
                    Minute = a.Minute
            }))).FirstOrDefaultAsync();
        }
    }
}
