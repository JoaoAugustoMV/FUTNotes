using System.Reflection.Metadata;
using FootNotes.Core.Domain;

namespace FootNotes.Match.Domain
{
    public class Match : Entity, IAggregateRoot
    {
        public Guid HomeTeamId { get; private set; }
        public Team HomeTeam { get; private set; }

        public Guid AwayTeamId { get; private set; }
        public Team AwayTeam { get; private set; }
        
        public Guid? CompetitionId { get; private set; }
        public Competition? Competition { get; private set; }

        public DateTime MatchDate { get; private set; }
        public MatchStatus Status { get; private set; }


        public Guid[]? AnnotationsIds { get; private set; }
        public Annotation[]? Annotations { get; private set; }



        public override bool IsValid(out string msg)
        {
            throw new NotImplementedException();
        }
    }

    public enum MatchStatus
    {
        Scheduled,
        FirstHalf,
        SecondHalf,
        FirstHalfProrrogation,
        SecondHalfProrrogation,
        Penalty,
        Suspended
    }
}
