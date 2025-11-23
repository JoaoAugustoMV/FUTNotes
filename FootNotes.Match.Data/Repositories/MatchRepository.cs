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
    public class MatchRepository(MatchContext _dbContext, IMediatorHandler mediatorHandler) 
        : RepositoryBase<Match, MatchContext>(_dbContext, mediatorHandler), IMatchRepository
    {
        public IQueryable<Match> GetAllByCodes(IEnumerable<string> codes)
        {
            return GetAll().Where(m => codes.Contains(m.Code));
        }
    }
}
