using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Application;
using FootNotes.MatchManagement.Domain.MatchModels;

namespace FootNotes.MatchManagement.Application.Requests
{
    public class CreateMatchManuallyRequest(
        Guid userId,
        string homeTeamName,
        string awayTeamName,
        string competitionName,
        DateTime? matchDate,
        MatchStatus status,
        MatchDecisionType? decisionType,
        uint? homeScore,
        uint? awayScore,
        uint? homePenaltyScore,
        uint? awayPenaltyScore
            ): IApiRequest
    {
        public Guid UserId { get; private set; } = userId;
        public string HomeTeamName { get; private set; } = homeTeamName;
        public string AwayTeamName { get; private set; } = awayTeamName;
        public string CompetitionName { get; private set; } = competitionName;
        public DateTime? MatchDate { get; private set; } = matchDate;
        public MatchStatus Status { get; private set; } = status;
        public MatchDecisionType? DecisionType { get; private set; } = decisionType;
        public uint? HomeScore { get; private set; } = homeScore;
        public uint? AwayScore { get; private set; } = awayScore;
        public uint? HomePenaltyScore { get; private set; } = homePenaltyScore;
        public uint? AwayPenaltyScore { get; private set; } = awayPenaltyScore;

        public bool IsValid(out string msg)
        {
            StringBuilder errorMsg = new();
            if (UserId == Guid.Empty)
                errorMsg.AppendLine("UserId is required;");
            if (string.IsNullOrWhiteSpace(HomeTeamName))
                errorMsg.AppendLine("HomeTeamName is required;");
            if (string.IsNullOrWhiteSpace(AwayTeamName))
                errorMsg.AppendLine("AwayTeamName is required;");

            if(HomeTeamName == AwayTeamName)
                errorMsg.AppendLine("HomeTeamName and AwayTeamName cannot be the same;");

            msg = errorMsg.ToString();

            return string.IsNullOrWhiteSpace(msg);
        }
    }
}
