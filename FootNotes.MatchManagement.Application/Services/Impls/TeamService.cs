using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.MatchManagement.Application.DTOs;
using FootNotes.MatchManagement.Application.Events.TeamEvents;
using FootNotes.MatchManagement.Domain.Repository;
using FootNotes.MatchManagement.Domain.TeamModels;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.MatchManagement.Application.Services.Impls
{
    public class TeamService(ITeamRepository teamRepository) : ITeamService
    {
        public async Task<Dictionary<string, Guid>> GetIdOrCreateTeamsAsync(TeamInfoDTO[] teamsInfo)
        {
            Dictionary<string, Guid> dict = await teamRepository.GetByTeamsCode(teamsInfo.Select(t => t.Code)).Select(t => new
            {
                t.Id,
                t.TeamCode
            }).ToDictionaryAsync(t => t.TeamCode, c => c.Id);

            List<Team> teamsToCreate = [];

            foreach (TeamInfoDTO teamInfo in teamsInfo)
            {
                if(!dict.TryGetValue(teamInfo.Code, out Guid _))
                {
                    Team team = Team.CreateNotManually(teamInfo.Name, teamInfo.Code);

                    team.AddEvent(new CreateTeamAutomaticEvent(team.Id, teamInfo.Name, teamInfo.Code));

                    teamsToCreate.Add(team);

                    dict.Add(teamInfo.Code, team.Id);
                }
            }

            if (teamsToCreate.Count > 0) 
            {
                await teamRepository.AddRangeAsync(teamsToCreate);
            }

            return dict;
        }
    }
}
