using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.AnnotationSessionModels;
using FootNotes.Core.Data;

namespace FootNotes.Annotations.Domain.Repositories
{
    public interface IAnnotationSessionRepository: IRepositoryBase<AnnotationSession>
    {
        Task AddAnnotations(IEnumerable<Annotation> annotations);
    }
}
