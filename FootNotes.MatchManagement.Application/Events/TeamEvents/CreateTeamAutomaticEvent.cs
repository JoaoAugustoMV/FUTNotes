using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;

namespace FootNotes.MatchManagement.Application.Events.TeamEvents
{
    public class CreateTeamAutomaticEvent(Guid teamId, string teamName) : Event(teamId)
    {
        public string TeamName { get; } = teamName;
    }
}
