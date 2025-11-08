using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.AnnotationSessionModels;
using FootNotes.Core.Application;
using FootNotes.Core.Messages;

namespace FootNotes.Annotations.Application.Commands.AnnotationSessionCommands
{
    public class CreateNewAnnotationSessionCommand : Command
    {
        public Guid UserId { get; set; }
        public Guid MatchId { get; set; }
        public AnnotationSessionType SessionType { get; set; }

        public override bool IsValid(out string msg)
        {
            StringBuilder err = new ();

            if (UserId == Guid.Empty)
                err.AppendLine("UserId is required;");

            if (MatchId == Guid.Empty)
                err.AppendLine("MatchId is required;");

            msg = err.ToString();
            return string.IsNullOrWhiteSpace(msg);            
        }
    }
}
