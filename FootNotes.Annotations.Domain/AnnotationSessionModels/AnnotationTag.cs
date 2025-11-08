using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Annotations.Domain.AnnotationSessionModels
{
    public class AnnotationTag(Guid annotationId, Guid tagId)
    {
        public Guid AnnotationId { get; private set; } = annotationId;
        public Guid TagId { get; private set; } = tagId;
    }

}
