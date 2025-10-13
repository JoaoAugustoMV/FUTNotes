using FootNotes.Core.Domain;

namespace FootNotes.Annotations.Domain.AnnotationSession
{
    public class AnnotationSession : Entity, IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public Guid MatchId { get; private set; }
        public DateTime Started { get; private set; }
        public DateTime? Ended { get; private set; }
        public AnnotationSessionStatus Status { get; private set; }
        public AnnotationSessionType Type { get; private set; }
        public virtual ICollection<Annotation> Annotations { get; private set; } = [];

        public override void ThrowIfInvalid()
        {
            throw new NotImplementedException();
        }
    }

    public enum AnnotationSessionStatus
    {
        Active,
        Paused,
        Completed
    }

    public enum AnnotationSessionType
    {
        Live,
        Reprise        
    }
}
