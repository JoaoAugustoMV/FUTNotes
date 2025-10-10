using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.Match.Domain
{
    public abstract class Professional: Entity
    {
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }

        public Guid TeamId { get; private set; }
        public Team Team { get; private set; }
    }
}
