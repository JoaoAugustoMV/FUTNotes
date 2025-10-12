using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.Annotations.Domain.MatchEvaluation
{
    public abstract class Rating: Entity
    {            
        public string? Comment { get; private set; }
        public double Rate { get; private set; }        
    }
}
