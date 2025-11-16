using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.MatchManagement.Adapters.MatchProviders.Dojo
{
    public record MatchInfoDojoApiResponse(IEnumerable<MatchInfoDojoResponse> events);
    
    public record MatchInfoDojoResponse(
        int startTimestamp,
        TournamentDojoResponse tournament,
        TeamDojoResponse homeTeam,
        TeamDojoResponse awayTeam
        );

    public record TournamentDojoResponse(string name);

    public record TeamDojoResponse(string name, string slug);
}
