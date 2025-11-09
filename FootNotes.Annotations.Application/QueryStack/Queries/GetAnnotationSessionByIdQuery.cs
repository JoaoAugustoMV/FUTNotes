using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Application.QueryStack.ViewModels;
using FootNotes.Core.Messages;
using MediatR;

namespace FootNotes.Annotations.Application.QueryStack.Queries
{
    public class GetAnnotationSessionByIdQuery : Query<AnnotationSessionViewModel>
    {
        public Guid AnnotationSessionId { get; private set; }
        public GetAnnotationSessionByIdQuery(Guid annotationSessionId)
        {
            AnnotationSessionId = annotationSessionId;
        }
    }
}
