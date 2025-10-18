using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Data.Context;
using FootNotes.Annotations.Domain.AnnotationSession;
using FootNotes.Annotations.Domain.TagModels;
using FootNotes.Core.Data;
using FootNotes.Core.Data.Communication;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.Annotations.Data.Repositories
{
    public class TagRepository(AnnotationsContext _dbContext, IMediatorHandler mediatorHandler) : 
        RepositoryBase<Tag>(_dbContext, mediatorHandler), IRepositoryBase<Tag>
    {
    }
}
