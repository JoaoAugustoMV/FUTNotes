using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;

namespace FootNotes.MatchManagement.Application.Events.MatchEvents
{
    public class InsertUpcomingMatchEvent(Guid aggregateId) : Event(aggregateId)
    {
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public Guid CompetitionId { get; set; }
        public DateTime MatchDate { get; set; }
    }
    
}
