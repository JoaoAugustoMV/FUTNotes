using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Annotations.Domain.MatchEvaluation
{
    public class PlayerRating : Rating
    {
        public Guid PlayerId { get; private set; }
        public override bool IsValid(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
