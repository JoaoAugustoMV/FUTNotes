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
using FootNotes.MatchManagement.Domain.TeamModels;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.MatchManagement.Data.Repositories
{
    public class TeamRepository(MatchContext dbContext, IMediatorHandler mediatorHandler) : 
        RepositoryBase<Team, MatchContext>(dbContext, mediatorHandler), ITeamRepository
    {
        public IQueryable<Team> GetByTeamsCode(string[] teamNames)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> GetIdByName(string name)
        {
            Guid teamId = Guid.Empty;

            var teamReturned =  await dbContext.Teams
                .Where(t => t.Name == name)
                .Select(t => new { t.Id})
                .FirstOrDefaultAsync();

            if (teamReturned != null)
            {
                teamId = teamReturned.Id;
            }

            return teamId;
        }
    }
}
