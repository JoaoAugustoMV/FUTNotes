using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Domain.MatchModels;

namespace FootNotes.MatchManagement.Application.Events.MatchEvents
{
    public class CreateMatchManuallyEvent: Event
    {        

        public CreateMatchManuallyEvent(
            Guid UserId,
            Guid aggregateId,
            Guid homeTeamId,
            Guid awayTeamId,
            DateTime? matchDate,
            MatchStatus status,
            MatchDecisionType? decisionType,
            uint? homeScore,
            uint? awayScore,
            uint? homePenaltyScore,
            uint? awayPenaltyScore

        ) : base(aggregateId)
        {
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            MatchDate = matchDate;
            Status = status;
            DecisionType = decisionType;
            HomeScore = homeScore;
            AwayScore = awayScore;
            HomePenaltyScore = homePenaltyScore;
            AwayPenaltyScore = awayPenaltyScore;
        }

        public Guid UserId { get; private set; }
        public Guid HomeTeamId { get; private set; }
        public Guid AwayTeamId { get; private set; }        
        public DateTime? MatchDate { get; private set; }
        public MatchStatus Status { get; private set; }
        public MatchDecisionType? DecisionType { get; private set; }
        public uint? HomeScore { get; private set; }
        public uint? AwayScore { get; private set; }
        public uint? HomePenaltyScore { get; private set; }
        public uint? AwayPenaltyScore { get; private set; }

    }
}
