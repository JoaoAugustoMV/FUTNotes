using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;

namespace FootNotes.MatchManagement.Application.Commands.MatchCommands
{
    public class FinishMatchCommand(Guid guid) : Command
    {
        public Guid MatchId { get; private set; } = guid;

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
