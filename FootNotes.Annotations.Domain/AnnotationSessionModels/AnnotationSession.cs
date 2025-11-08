using System.IO.Pipes;
using System.Text;
using FootNotes.Core.Domain;

namespace FootNotes.Annotations.Domain.AnnotationSessionModels
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

        #region Domain Methods
        public void EndSession()
        {
            if (Status == AnnotationSessionStatus.Completed)
            {
                throw new InvalidOperationException("Annotation session is already completed.");
            }
            Ended = DateTime.UtcNow;
            Status = AnnotationSessionStatus.Completed;
        }

        public void PauseSession()
        {
            if (Status != AnnotationSessionStatus.Active)
            {
                throw new InvalidOperationException("Only active sessions can be paused.");
            }
            Status = AnnotationSessionStatus.Paused;
        }

        public void ResumeSession()
        {
            if (Status != AnnotationSessionStatus.Paused)
            {
                throw new InvalidOperationException("Only paused sessions can be resumed.");
            }
            Status = AnnotationSessionStatus.Active;            
        }

        public void AddAnnotation(string description, int? minute,AnnotationType type)
        {
            if (Status != AnnotationSessionStatus.Active)
            {
                throw new InvalidOperationException("Annotations can only be added to active sessions.");
            }

            Annotations.Add(CreateNewAnnotation(description, minute, type));
        }
        #endregion

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

        private Annotation CreateNewAnnotation(string description, int? minute, AnnotationType type)
        {
            Annotation annotation = new(Id, description, minute, type);
            
            annotation.ThrowIfInvalid();

            return annotation;
            
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
