using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.MatchManagement.Domain.TeamModels
{
    public class Team : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string? ShortName { get; private set; }
        public bool HasCreatedManually { get; private set; }
        public Guid[]? PlayersId { get; private set; }
        public Player[]? Players { get; private set; }

        public Guid? CoachId { get; private set; }

        protected Team()
        {
            
        }
        
        public override void ThrowIfInvalid()
        {
            StringBuilder errorMsg = new();
            if (string.IsNullOrWhiteSpace(Name))
                errorMsg.AppendLine("Name is required;");
            string msg = errorMsg.ToString();

            if(!string.IsNullOrEmpty(msg)){
                throw new EntityInvalidException(msg);
            }
        }

        #region Factory Methods
        public static Team CreateManually(string teamName)
        {
            Team team = new()
            {
                Name = teamName,
                HasCreatedManually = true
            };

            team.ThrowIfInvalid();
            
            return team;
        }

        public static Team CreateNotManually(string teamName)
        {
            Team team = new()
            {
                Name = teamName,
                HasCreatedManually = false
            };

            team.ThrowIfInvalid();

            return team;
        }
        #endregion

    }
}
