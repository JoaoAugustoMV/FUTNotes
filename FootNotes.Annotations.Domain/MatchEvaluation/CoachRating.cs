using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Annotations.Domain.MatchEvaluation
{
    public class CoachRating : Rating
    {
        public Guid CoachId { get; private set; }
        public override bool IsValid(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
