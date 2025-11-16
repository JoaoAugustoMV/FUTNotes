using System.Reflection.Metadata;
using System.Text;
using FootNotes.Core.Domain;

namespace FootNotes.MatchManagement.Domain.MatchModels
{
    public class Match : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
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

        public bool HasCreatedManually { get; private set; }


        public override void ThrowIfInvalid()
        {
            StringBuilder error = new();

            if (HomeTeamId == AwayTeamId)
                error.Append("Home Team and Away Team must be different");

            if(!HasCreatedManually && string.IsNullOrEmpty(Code))
            {
                error.Append("Code is required;");
            }

            string msg = error.ToString();

            if(msg.Length > 0)
            {
                throw new EntityInvalidException(msg);
            }            
        }

        #region Methods
        public void UpdateStatus(MatchStatus status)
        {
            Status = status;            
        }

        public void UpdateHomeScore()
        {
            HomeScore++;
        }

        public void UpdateAwayScore()
        {
            AwayScore++;
        }


        #endregion

        #region Static Methods
        public static string GenerateCode(string homeTeamCode, string awayTeamCode, DateTime date)
        {
            return string.Concat(homeTeamCode, "_", awayTeamCode, "_", date.ToString("yyyyMMdd"));
        }

        #endregion

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

        public static Match CreateUpcoming(
            string code,
            Guid homeTeamId,
            Guid awayTeamId,
            Guid competitionId,
            DateTime matchDate
    )
        {
            Match match = new()
            {
                Code = code,
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId,
                MatchDate = matchDate,
                Status = MatchStatus.Scheduled,
                HomeScore = 0,
                AwayScore = 0,
                CompetitionId = competitionId,
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
        HalfTime,
        SecondHalf,
        Finished,
        WaitingForProrrogation,
        FirstHalfProrrogation,
        HalfTimeProrrogation,
        SecondHalfProrrogation,
        WaitingForPenalty,
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
