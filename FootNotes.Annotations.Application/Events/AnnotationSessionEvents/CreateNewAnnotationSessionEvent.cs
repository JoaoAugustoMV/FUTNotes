using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.AnnotationSession;
using FootNotes.Core.Application;
using FootNotes.Core.Messages;

namespace FootNotes.Annotations.Application.Events.AnnotationSessionEvents
{
    public class CreateNewAnnotationSessionEvent(
        Guid aggregateId,
        Guid userId,
        Guid matchId,
        AnnotationSessionType sessionType,
        DateTime started) : Event(aggregateId)
    {        
        public Guid UserId { get; set; } = userId;
        public Guid MatchId { get; set; } = matchId;
        public AnnotationSessionType SessionType { get; set; } = sessionType;
        public DateTime Started { get; set; } = started;
    }
}
