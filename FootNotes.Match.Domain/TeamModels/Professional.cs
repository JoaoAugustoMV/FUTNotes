using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.MatchManagement.Domain.TeamModels
{
    public abstract class Professional: Entity
    {
        public string Name { get; private set; }
        public DateTime? BirthDate { get; private set; }

        public Guid TeamId { get; private set; }        
    }
}
