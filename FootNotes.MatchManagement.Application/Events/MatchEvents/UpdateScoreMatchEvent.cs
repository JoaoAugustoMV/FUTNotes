using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Domain.MatchModels;

namespace FootNotes.MatchManagement.Application.Events.MatchEvents
{
    public class UpdateScoreMatchEvent(Guid matchId, Guid teamId, uint homeScore, uint awayScore) : Event(matchId)
    {        
        public Guid MatchId { get; } = matchId;
        public Guid TeamId { get; } = teamId;
        public uint HomeScore { get; } = homeScore;
        public uint AwayScore {get;} = awayScore;

    }
}
