using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Data.Context;
using FootNotes.Annotations.Domain.AnnotationSession;
using FootNotes.Core.Data;
using FootNotes.Core.Data.Communication;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.Annotations.Data.Repositories
{
    public class AnnotationSessionRepository(AnnotationsContext _dbContext, IMediatorHandler mediatorHandler) : 
        RepositoryBase<AnnotationSession>(_dbContext, mediatorHandler), IAnnotationSessionRepository
    {
    }
}
