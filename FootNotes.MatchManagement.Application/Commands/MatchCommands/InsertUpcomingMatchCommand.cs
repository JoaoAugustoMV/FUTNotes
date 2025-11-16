using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Application.DTOs;

namespace FootNotes.MatchManagement.Application.Commands.MatchCommands
{
    public class InsertUpcomingMatchCommand: Command
    {
        public TeamInfoDTO HomeTeamInfo { get; set; }
        public TeamInfoDTO AwayTeamInfo { get; set; }
        public Guid CompetitionId { get; set; }
        public DateTime MatchDate { get; set; }

        public override bool IsValid(out string msg)
        {
            StringBuilder error = new();

            if (string.IsNullOrEmpty(HomeTeamInfo.Name))
                error.Append("Home Team Name is required. "); 
            if (string.IsNullOrEmpty(AwayTeamInfo.Name))            
                error.Append("Away Team Name is required. ");

            if (string.IsNullOrEmpty(HomeTeamInfo.Code))
                error.Append("Home Team Name is required. ");
            if (string.IsNullOrEmpty(AwayTeamInfo.Code))
                error.Append("Away Team Name is required. ");

            if (HomeTeamInfo == AwayTeamInfo)
                error.Append("Home Team and Away Team must be different. ");

            msg = error.ToString();
            return msg.Length == 0;
        }
    }
}
