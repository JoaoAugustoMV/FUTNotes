using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Annotations.Domain.MatchEvaluation
{
    public class RefereeRating : Rating
    {
        public Guid RefereeId { get; private set; }
        public override void ThrowIfInvalid()
        {
            throw new NotImplementedException();
        }
    }
}
