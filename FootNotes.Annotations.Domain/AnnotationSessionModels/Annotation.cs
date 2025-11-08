using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.TagModels;
using FootNotes.Core.Domain;

namespace FootNotes.Annotations.Domain.AnnotationSessionModels
{
    public class Annotation : Entity
    {
        public Guid AnnotationSessionId { get; private set; }        
        public virtual AnnotationSession AnnotationSession { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public AnnotationType Type { get; private set; }
        public string? Description { get; private set; }
        public int? Minute { get; private set; }

        public ICollection<Tag> Tags { get; private set; } = [];

        internal Annotation(Guid annotationSessionId, string description, int? minute, AnnotationType type)
        {
            AnnotationSessionId = annotationSessionId;
            Description = description;
            Minute = minute;
            Type = type;
            TimeStamp = DateTime.UtcNow;

        }

        public override void ThrowIfInvalid()
        {
            StringBuilder error = new();

            if(AnnotationSessionId == Guid.Empty)
            {
                error.Append("SessionId is required");
            }

            if(Minute.HasValue && Minute < 0)
            {
                error.AppendLine("Minute must be positive");
            }

            string msgError = error.ToString();
            if (!string.IsNullOrEmpty(msgError))
            {
                throw new EntityInvalidException(msgError);
            }
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
