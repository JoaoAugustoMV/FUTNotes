using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.TagModels;
using FootNotes.Core.Domain;

namespace FootNotes.Annotations.Domain.AnnotationSession
{
    public class Annotation : Entity
    {
        public Guid AnnotationSessionId { get; private set; }        
        public virtual AnnotationSession AnnotationSession { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public AnnotationType Type { get; private set; }
        public string? Description { get; private set; }

        public ICollection<Tag> Tags { get; private set; } = [];

        public override void ThrowIfInvalid()
        {
            throw new NotImplementedException();
        }
    }

    public enum AnnotationType : byte
    {
        General = 0,
        PlayerPerformance = 1,
        TeamTactics = 2,
        RefereeDecision = 3
    }
}
