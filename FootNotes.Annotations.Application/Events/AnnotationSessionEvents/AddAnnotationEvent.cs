using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.AnnotationSessionModels;
using FootNotes.Core.Messages;

namespace FootNotes.Annotations.Application.Events.AnnotationSessionEvents
{
    public class AddAnnotationEvent: Event
    {
        public Guid AnnotationSessionId { get; private set; }
        public string Description { get; private set; }
        public int? Minute { get; private set; }
        public AnnotationType Type { get; private set; }
        public AddAnnotationEvent(Guid annotationSessionId, string description, int? minute, AnnotationType type): base(annotationSessionId)
        {
            AnnotationSessionId = annotationSessionId;
            Description = description;
            Minute = minute;
            Type = type;
        }
    }
}
