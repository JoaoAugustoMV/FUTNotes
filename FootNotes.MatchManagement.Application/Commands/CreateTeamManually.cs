using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;

namespace FootNotes.MatchManagement.Application.Commands
{
    public class CreateTeamManuallyCommand(string teamName) : Command
    {
        public string TeamName { get; private set; } = teamName;
        public override bool IsValid(out string msg)
        {
            StringBuilder errorMsg = new();
            
            if (string.IsNullOrWhiteSpace(TeamName))
                errorMsg.AppendLine("TeamName is required;");

            msg = errorMsg.ToString();
            return string.IsNullOrWhiteSpace(msg);            
        }
    }
}
