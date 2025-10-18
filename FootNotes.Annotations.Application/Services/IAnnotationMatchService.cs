using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Annotations.Application.Services
{
    public interface IAnnotationMatchService
    {       
        Task<bool> ExistsMatchId(Guid matchId);
    }
}
