using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;

namespace FootNotes.MatchManagement.Application.Commands.MatchCommands
{
    public class InsertUpcomingMatchCommand: Command
    {
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public Guid CompetitionId { get; set; }
        public DateTime MatchDate { get; set; }

        public override bool IsValid(out string msg)
        {
            StringBuilder error = new();

            if (string.IsNullOrEmpty(HomeTeamName))
                error.Append("Home Team Name is required. "); 
            if (string.IsNullOrEmpty(AwayTeamName))            
                error.Append("Away Team Name is required. ");

            if(HomeTeamName == AwayTeamName)
                error.Append("Home Team and Away Team must be different. ");

            msg = error.ToString();
            return msg.Length == 0;
        }
    }
}
