using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Application.DTOs;

namespace FootNotes.MatchManagement.Application.Commands.MatchCommands
{
    public class InsertUpcomingMatchsCommand: Command
    {
        public IEnumerable<UpcomingMatchInfo> MatchInfos { get; set; }

        public override bool IsValid(out string msg)
        {
            StringBuilder error = new();

            if (MatchInfos != null) {
                foreach (UpcomingMatchInfo info in MatchInfos)
                {
                    if (string.IsNullOrEmpty(info.HomeTeamInfo.Name))
                        error.Append("Home Team Name is required. "); 
                    if (string.IsNullOrEmpty(info.AwayTeamInfo.Name))            
                        error.Append("Away Team Name is required. ");

                    if (string.IsNullOrEmpty(info.HomeTeamInfo.Code))
                        error.Append("Home Team Name is required. ");
                    if (string.IsNullOrEmpty(info.AwayTeamInfo.Code))
                        error.Append("Away Team Name is required. ");

                    if (info.HomeTeamInfo == info.AwayTeamInfo)
                        error.Append("Home Team and Away Team must be different. ");                    
                }
            }


            msg = error.ToString();
            return msg.Length == 0;
        }
    }

    public record UpcomingMatchInfo(TeamInfoDTO HomeTeamInfo, TeamInfoDTO AwayTeamInfo, Guid CompetitionId, DateTime MatchDate);
}
