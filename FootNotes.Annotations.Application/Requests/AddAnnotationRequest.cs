using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.AnnotationSessionModels;
using FootNotes.Core.Application;

namespace FootNotes.Annotations.Application.Requests
{
    public class AddAnnotationRequest : IApiRequest
    {
        public Guid AnnotationSessionId { get; set; }
        public string Description { get; set; }
        public int? Minute { get; set; }
        public AnnotationType Type {get; set; }

        public bool IsValid(out string msg)
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
