using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.AnnotationSessionModels;
using FootNotes.Core.Messages;

namespace FootNotes.Annotations.Application.Commands.AnnotationSessionCommands
{
    public class AddAnnotationCommand: Command
    {
        public Guid AnnotationSessionId { get; set; }
        public string Description { get; set; }
        public int? Minute { get; set; }
        public AnnotationType Type { get; set; }

        public override bool IsValid(out string msg)
        {
            StringBuilder error = new();
            if (AnnotationSessionId == Guid.Empty)
            {
                error.AppendLine("AnnotationSessionId is required.");
            }

            msg = error.ToString();
            return string.IsNullOrEmpty(msg);            
        }
    }
}
