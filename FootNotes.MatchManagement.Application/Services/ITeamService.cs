using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.MatchManagement.Application.DTOs;

namespace FootNotes.MatchManagement.Application.Services
{
    public interface ITeamService
    {
        Task<Dictionary<string, Guid>> GetIdOrCreateTeamsAsync(TeamInfoDTO[] teamsInfo);
    }
}
