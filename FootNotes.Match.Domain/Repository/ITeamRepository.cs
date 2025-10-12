using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Data;
using FootNotes.MatchManagement.Domain.MatchModels;
using FootNotes.MatchManagement.Domain.TeamModels;

namespace FootNotes.MatchManagement.Domain.Repository
{
    public interface ITeamRepository: IRepositoryBase<Team>
    {
        Task<Guid> GetIdByName(string name);
    }
}
