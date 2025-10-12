using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.Annotations.Domain.AnnotationSession
{
    public class Annotation : Entity
    {
        public Guid? PlayerId { get; private set; }
        public DateTime TimeStamp { get; private set; }
        //public AnnotationType Type { get; private set; }
        public string? Description { get; private set; }

        public override void ThrowIfInvalid()
        {
            throw new NotImplementedException();
        }
    }
}
