using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Domain.MatchModels;

namespace FootNotes.MatchManagement.Application.Commands.MatchCommands
{
    public class UpdateMatchStatusCommand(Guid guid, MatchStatus matchStatus) : Command
    {
        public Guid MatchId { get; private set; } = guid;
        public MatchStatus NewStatus { get; private set; } = matchStatus;

        public override bool IsValid(out string msg)
        {
            StringBuilder errorMsg = new();

            if (MatchId == Guid.Empty)
                errorMsg.AppendLine("MatchId is required;");

            msg = errorMsg.ToString();
            return string.IsNullOrWhiteSpace(msg);            
        }
    }
}
