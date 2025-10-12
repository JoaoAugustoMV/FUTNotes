using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Annotations.Domain.MatchEvaluation
{
    public class TeamRating : Rating
    {
        public Guid TeamId { get; private set; }
        public IEnumerable<PlayerRating>? PlayersEvaluations { get; private set; }
        public CoachRating? CoachEvaluation { get; private set; }
        public override void ThrowIfInvalid()
        {
            throw new NotImplementedException();
        }
    }
}
