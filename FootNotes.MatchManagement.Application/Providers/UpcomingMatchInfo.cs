using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.MatchManagement.Application.Providers
{
    public record UpcomingMatchInfo(        
        Guid CompetitionId,
        DateTime MatchDate,
        string HomeCode,
        string AwayCode
    )
    {
    }
}
