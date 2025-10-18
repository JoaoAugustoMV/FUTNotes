using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.AnnotationSession;
using FootNotes.Core.Application;

namespace FootNotes.Annotations.Application.Requests
{
    public class CreateNewAnnotationSessionRequest: IApiRequest
    {
        public Guid UserId { get; set; }
        public Guid MatchId { get; set; }
        public AnnotationSessionType SessionType { get; set; }

        public bool IsValid(out string msg)
        {
            StringBuilder err = new StringBuilder();
            if (UserId == Guid.Empty)
                err.AppendLine("UserId is required;");

            if (MatchId == Guid.Empty)
                err.AppendLine("MatchId is required;");
            msg = err.ToString();
            return string.IsNullOrWhiteSpace(msg);            
        }
    }
}
