using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.Match.Domain
{
    abstract class Rate: Entity
    {
        public Guid MatchId { get; private set; }
        public Guid UserId { get; private set; }        
        public string? Comment { get; private set; }
        
        public TypeRate TypeRate { get; private set; }
    }

    public enum TypeRate
    {
        Match,
        Team,
        Player,
        Referee
    }
}
