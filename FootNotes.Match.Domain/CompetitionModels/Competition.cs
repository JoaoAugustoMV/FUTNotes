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
        public override void ThrowIfInvalid()
        {
            StringBuilder msgError = new();
            
            if (string.IsNullOrWhiteSpace(Name))
                msgError.AppendLine("Competition name cannot be null or empty.");

            string msgStr = msgError.ToString();

            if (!string.IsNullOrWhiteSpace(msgStr))
                throw new EntityInvalidException(msgStr);
        }

        public Competition(string name)
        {
            Name = name;
        }

        public static Competition Create(string name, CompetitionScope scope, CompetitionType type, string? season = null)
        {
            Competition competition = new(name)
            {
                Scope = scope,
                Type = type,
                Season = season
            };

            competition.ThrowIfInvalid();
            return competition;
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
