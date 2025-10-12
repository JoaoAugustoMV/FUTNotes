using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Domain.MatchModels;

namespace FootNotes.MatchManagement.Application.Commands.MatchCommands
{
    public class CreateMatchManuallyCommand(
        Guid userId,
        Guid homeTeamId,
        Guid awayTeamId,        
        DateTime? matchDate,
        MatchStatus status,
        MatchDecisionType? decisionType,
        uint? homeScore,
        uint? awayScore,
        uint? homePenaltyScore,
        uint? awayPenaltyScore
            ) : Command
    {
        public Guid UserId { get; private set; } = userId;
        public Guid HomeTeamId { get; private set; } = homeTeamId;
        public Guid AwayTeamId { get; private set; } = awayTeamId;        
        public DateTime? MatchDate { get; private set; } = matchDate;
        public MatchStatus Status { get; private set; } = status;
        public MatchDecisionType? DecisionType { get; private set; } = decisionType;
        public uint? HomeScore { get; private set; } = homeScore;
        public uint? AwayScore { get; private set; } = awayScore;
        public uint? HomePenaltyScore { get; private set; } = homePenaltyScore;
        public uint? AwayPenaltyScore { get; private set; } = awayPenaltyScore;

        public override bool IsValid(out string msg)
        {
            StringBuilder errorMsg = new();
            if (UserId == Guid.Empty)
                errorMsg.AppendLine("UserId is required;");
            if (HomeTeamId == Guid.Empty)
                errorMsg.AppendLine("HomeTeamId is required;");
            if (AwayTeamId == Guid.Empty)
                errorMsg.AppendLine("AwayTeamId is required;");

            if(HomeTeamId == AwayTeamId)
                errorMsg.AppendLine("HomeTeamId and AwayTeamId cannot be the same;");
            
            msg = errorMsg.ToString();

            return string.IsNullOrWhiteSpace(msg);
        }
    }
}
