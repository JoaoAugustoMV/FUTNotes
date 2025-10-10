using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.Match.Domain
{
    public class Team : Entity
    {
        public string Name { get; private set; }
        public string? ShortName { get; private set; }

        public Guid[]? PlayersId { get; private set; }
        public Player[]? Players { get; private set; }

        public Guid CoachId { get; private set; }
        public Coach Coach { get; private set; }

        public override bool IsValid(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
