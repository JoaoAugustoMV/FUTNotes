using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.MatchManagement.Application.Events.TeamEvents;
using FootNotes.MatchManagement.Domain.Repository;
using FootNotes.MatchManagement.Domain.TeamModels;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.MatchManagement.Application.Services.Impls
{
    public class TeamService(ITeamRepository teamRepository) : ITeamService
    {
        public async Task<Dictionary<string, Guid>> GetIdOrCreateTeamsAsync(string[] teamsName)
        {
            Dictionary<string, Guid> dict = await teamRepository.GetByTeamsName(teamsName).Select(t => new
            {
                t.Id,
                t.Name
            }).ToDictionaryAsync(t => t.Name, c => c.Id);

            List<Team> teamsToCreate = [];

            foreach (string teamName in teamsName)
            {
                if(!dict.TryGetValue(teamName, out Guid _))
                {
                    Team team = Team.CreateNotManually(teamName);

                    team.AddEvent(new CreateTeamAutomaticEvent(team.Id, teamName));

                    teamsToCreate.Add(team);

                    dict.Add(teamName, team.Id);
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
