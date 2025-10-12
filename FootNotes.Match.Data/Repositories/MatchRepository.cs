using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Data;
using FootNotes.Core.Data.Communication;
using FootNotes.MatchManagement.Data.Context;
using FootNotes.MatchManagement.Domain.MatchModels;
using FootNotes.MatchManagement.Domain.Repository;

namespace FootNotes.MatchManagement.Data.Repositories
{
    public class MatchRepository(MatchContext _dbContext, IMediatorHandler mediatorHandler) : RepositoryBase<Match>(_dbContext, mediatorHandler), IMatchRepository
    {
    }
}
