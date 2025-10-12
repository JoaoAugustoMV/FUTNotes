using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Application;
using FootNotes.MatchManagement.Domain.MatchModels;

namespace FootNotes.MatchManagement.Application.Requests
{
    public class UpdateResultMatchRequest: IApiRequest
    {
        public Guid MatchId { get; set; }
        public MatchStatus NewStatus { get; set; }

        public bool IsValid(out string msg)
        {
            StringBuilder errorMsg = new();

            if (MatchId == Guid.Empty)
                errorMsg.AppendLine("MatchId is required;");

            msg = errorMsg.ToString();
            return string.IsNullOrEmpty(msg);
        }
    }
}
