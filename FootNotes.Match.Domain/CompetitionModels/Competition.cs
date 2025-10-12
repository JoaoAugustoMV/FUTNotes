using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.MatchManagement.Domain.CompetitionModels
{
    public class Competition: Entity, IAggregateRoot
    {
        public string Name { get; private set; }        
        public string? Season { get; private set; }
        public CompetitionScope Scope { get; private set; }
        public CompetitionType Type { get; private set; }        
        public override bool IsValid(out string msg)
        {
            throw new NotImplementedException();
        }

        public Competition(string name)
        {
            Name = name;
        }
    }

    public enum CompetitionScope
    {
        Local = 1,
        Regional = 2,
        National = 3,
        Continental = 4,
        International = 5,
        Global = 6 
    }

    public enum CompetitionType
    {
        Clubs = 1,
        NationalTeams = 2
    }
}
