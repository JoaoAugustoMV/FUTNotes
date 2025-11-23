using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.MatchManagement.Application.DTOs;

namespace FootNotes.MatchManagement.Application.Providers
{
    public record UpcomingMatchInfo(        
        Guid CompetitionId,
        DateTime MatchDate,
        TeamInfoDTO HomeTeamInfo,
        TeamInfoDTO AwayTeamInfo
    )
    {
    }    
}
