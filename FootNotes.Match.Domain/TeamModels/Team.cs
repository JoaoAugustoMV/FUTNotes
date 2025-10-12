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
        
        public override bool IsValid(out string msg)
        {
            StringBuilder errorMsg = new();
            if (string.IsNullOrWhiteSpace(Name))
                errorMsg.AppendLine("Name is required;");
            msg = errorMsg.ToString();

            return string.IsNullOrEmpty(msg);            
        }

        #region Factory Methods
        public static Team CreateManually(string teamName)
        {
            Team team = new()
            {
                Name = teamName,
                HasCreatedManually = true
            };

            if (!team.IsValid(out string msg))
            {
                throw new EntityInvalidException(msg);
            }

            return team;
        }
        #endregion

    }
}
