using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Application.Requests;
using FootNotes.Core.Application;

namespace FootNotes.Annotations.Application.Services
{
    public interface IAnnotationSessionService
    {
        Task<Result<Guid>> CreateNewAnnotationSessionAsync(CreateNewAnnotationSessionRequest request);
        Task<Result<Guid>> AddAnnotationAsync(AddAnnotationRequest request);
    }
}
