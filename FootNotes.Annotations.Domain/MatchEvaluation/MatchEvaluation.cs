using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Annotations.Domain.MatchEvaluation
{
    public class MatchEvaluation
    {
        public Guid MatchId { get; private set; }
        public Guid UserId { get; private set; }        
        public TeamRating? HomeTeamEvaluation { get; private set; }
        public TeamRating? AwayTeamEvaluation { get; private set; }
        public MatchRating? MatchOverallEvaluation { get; private set; }
        public MatchRating? FunEvaluation { get; private set; }
        public RefereeRating? RefereeEvaluation { get; private set; }
        public DateTime Created { get; private set; }

    }
}
