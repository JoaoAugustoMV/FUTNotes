using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Application;
using FootNotes.MatchManagement.Domain.MatchModels;

namespace FootNotes.MatchManagement.Application.Requests
{
    public class UpdateScoreMatchRequest : IApiRequest
    {
        // TODO: Add Player that scored the goal, the one who assist if possible and the minute of the goal
        public Guid MatchId { get; set; }
        public Guid TeamId { get; set; }

        public bool IsValid(out string msg)
        {
            StringBuilder errorMsg = new();

            if (MatchId == Guid.Empty)
                errorMsg.AppendLine("MatchId is required;");

            if (TeamId == Guid.Empty)
                errorMsg.AppendLine("TeamId is required;");

            msg = errorMsg.ToString();
            return string.IsNullOrEmpty(msg);
        }
    }
}
