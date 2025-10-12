using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Domain.MatchModels;

namespace FootNotes.MatchManagement.Application.Events.MatchEvents
{
    public class UpdateMatchStatusEvent(Guid MatchId, MatchStatus NewStatus) : Event(MatchId)
    {
        public MatchStatus NewStatus { get; } = NewStatus;
    }
}
