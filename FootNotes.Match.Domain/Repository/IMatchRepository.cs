using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Data;
using FootNotes.MatchManagement.Domain.MatchModels;

namespace FootNotes.MatchManagement.Domain.Repository
{
    public interface IMatchRepository: IRepositoryBase<Match>
    {
        public IQueryable<Match> GetAllByCodes(IEnumerable<string> codes);
    }
}
