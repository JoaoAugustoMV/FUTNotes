using System.Text;
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
            StringBuilder error = new();

            if(UserId == Guid.Empty)
            {
                error.Append("UserId is required;");
            }

            if(MatchId== Guid.Empty)
            {
                error.Append("MatchId is required;");
            }

            if(Started == DateTime.MinValue)
            {
                error.Append("Started is required");
            }

            string msgError = error.ToString();
            if (!string.IsNullOrEmpty(msgError))
            {
                throw new EntityInvalidException(msgError);
            }
            
        }

        #region Factory Methods
        public static AnnotationSession CreateNew(Guid userId, Guid matchId, AnnotationSessionType sessionType)
        {
            AnnotationSession session = new()
            {
                UserId = userId,
                MatchId = matchId,
                Started = DateTime.UtcNow,
                Status = AnnotationSessionStatus.Active,
                Type = sessionType
            };

            session.ThrowIfInvalid();

            return session;
        }
        #endregion
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
