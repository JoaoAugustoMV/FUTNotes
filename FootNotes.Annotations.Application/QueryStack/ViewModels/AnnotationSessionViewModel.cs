using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FootNotes.Annotations.Domain.AnnotationSessionModels;
using FootNotes.Annotations.Domain.TagModels;

namespace FootNotes.Annotations.Application.QueryStack.ViewModels
{
    public record AnnotationSessionViewModel(
        Guid UserId,
        Guid MatchId,
        DateTime Started,
        DateTime? Ended,
        AnnotationSessionStatus Status,
        AnnotationSessionType Type,
        IEnumerable<AnnotationViewModel> Annotations
        );
    
    public record AnnotationViewModel() 
    {
        public DateTime TimeStamp { get; set; }
        public AnnotationType Type { get; set; }
        public string? Description { get; set; }
        public int? Minute { get; set; }
        public ICollection<TagViewModel> Tags { get; set; }
    }

    public class TagViewModel
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }         
        public DateTime CreatedAt { get; set; }
    }

}
