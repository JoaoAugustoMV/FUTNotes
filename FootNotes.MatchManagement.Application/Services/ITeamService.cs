using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.MatchManagement.Application.Services
{
    public interface ITeamService
    {
        Task<Dictionary<string, Guid>> GetIdOrCreateTeamsAsync(string[] teamsName);
    }
}
