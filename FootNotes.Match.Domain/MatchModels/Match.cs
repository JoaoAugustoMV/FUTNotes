using System.Reflection.Metadata;
using FootNotes.Core.Domain;

namespace FootNotes.MatchManagement.Domain.MatchModels
{
    public class Match : Entity, IAggregateRoot
    {
        public Guid HomeTeamId { get; private set; }

        public Guid AwayTeamId { get; private set; }        
        
        public Guid? CompetitionId { get; private set; }        

        public DateTime? MatchDate { get; private set; }
        public MatchStatus Status { get; private set; }

        public MatchDecisionType? DecisionType { get; private set; }
        public uint? HomeScore { get; private set; }
        public uint? AwayScore { get; private set; }

        
        public uint? HomePenaltyScore { get; private set; }
        public uint? AwayPenaltyScore { get; private set; }


        public override void ThrowIfInvalid()
        {
            throw new NotImplementedException();
        }

        #region Factory Methods
        public static Match CreateManually(            
            Guid homeTeamId,
            Guid awayTeamId,
            DateTime? matchDate,
            MatchStatus status,
            MatchDecisionType? decisionType,
            uint? homeScore,
            uint? awayScore,
            uint? homePenaltyScore,
            uint? awayPenaltyScore

            )
        {
            Match match = new()
            {
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId,
                MatchDate = matchDate,
                Status = status,
                DecisionType = decisionType,
                HomeScore = homeScore,
                AwayScore = awayScore,
                HomePenaltyScore = homePenaltyScore,
                AwayPenaltyScore = awayPenaltyScore
            };

            match.ThrowIfInvalid();            

            return match;
        }
        #endregion
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

    public enum MatchDecisionType : byte
    {
        RegularTime = 0,
        ExtraTime = 1,
        PenaltyShootout = 2
    }

}
