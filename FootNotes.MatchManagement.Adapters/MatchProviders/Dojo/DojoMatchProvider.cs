using System.Net.Http.Json;
using FootNotes.MatchManagement.Application.DTOs;
using FootNotes.MatchManagement.Application.Providers;
using FootNotes.MatchManagement.Domain.TeamModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FootNotes.MatchManagement.Adapters.MatchProviders.Dojo
{
    public class DojoMatchProvider(HttpClient httpClient, IOptionsMonitor<DojoConfigs> optionsMonitor) : IMatchProvider
    {        
        private readonly DojoConfigs options = optionsMonitor.CurrentValue;

        public async Task<IEnumerable<UpcomingMatchInfo>> GetUpcomingMatchs()
        {
            MatchInfoDojoApiResponse?[]? competitionMatchResults = await RequestMatchsInfo();

            List<UpcomingMatchInfo> allMatches = [];

            for (int i = 0; i < options.AvailableCompetition.Length; i++)
            {
                if (competitionMatchResults != null && competitionMatchResults[i] != null)
                {
                    allMatches.AddRange(
                        competitionMatchResults[i]!.events
                        .Select(e => new UpcomingMatchInfo(
                            options.AvailableCompetition[i].InternalId,
                            DateTime.Parse(e.startTimestamp.ToString()),
                            new TeamInfoDTO(e.homeTeam.name, Team.GenerateTeamCode(e.homeTeam.name)),
                            new TeamInfoDTO(e.awayTeam.name, Team.GenerateTeamCode(e.awayTeam.name)))));
                }
            }

            return allMatches;
        }

        private async Task<MatchInfoDojoApiResponse?[]?> RequestMatchsInfo()
        {
            List<Task<MatchInfoDojoApiResponse?>> tasks = [];

            foreach (DojoConfigCompetition item in options.AvailableCompetition)
            {
                tasks.Add(
                    httpClient.GetFromJsonAsync<MatchInfoDojoApiResponse>(GetUriFormat(item.Name)
                    ));
            }

            MatchInfoDojoApiResponse?[]? competitionMatchResults = await Task.WhenAll(tasks);
            return competitionMatchResults;
        }

        private string GetUriFormat(string competitionName)
        {
            DojoConfigCompetition competition = options.AvailableCompetition
                .First(c => c.Name.Equals(competitionName, StringComparison.OrdinalIgnoreCase));

            return string.Concat(options.URI_Base,
                options.Path_NextMatches,
                $"pageIndex=0&tornamentId={competition.ExternalId}&seasonId={competition.ExternalCurrentSessionId}");                
        }
    }
}
