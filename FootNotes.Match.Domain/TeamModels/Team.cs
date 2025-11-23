using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;
using FootNotes.Crosscutting.Utils;

namespace FootNotes.MatchManagement.Domain.TeamModels
{
    public class Team : Entity, IAggregateRoot
    {
        public string TeamCode { get; private set; }
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

            if (string.IsNullOrWhiteSpace(TeamCode) && !HasCreatedManually)
            {
                errorMsg.AppendLine("TeamCode is required;");
            }            
            
            string msg = errorMsg.ToString();

            if(!string.IsNullOrEmpty(msg)){
                throw new EntityInvalidException(msg);
            }
        }

        public static string GenerateTeamCode(string teamName)
        {
            if (string.IsNullOrWhiteSpace(teamName))
            {
                throw new ArgumentException("Team name cannot be null or empty.", nameof(teamName));
            }
            // Generate a simple team code by taking the first three letters of the team name
            return teamName.RemoveAccents().ToLower().Replace(" ", "-");
        }

        #region Factory Methods
        public static Team CreateManually(string teamName)
        {
            Team team = new()
            {
                Name = teamName,
                HasCreatedManually = true,
                TeamCode = string.Empty
            };

            team.ThrowIfInvalid();
            
            return team;
        }

        public static Team CreateNotManually(string teamName, string teamCode)
        {
            Team team = new()
            {
                Name = teamName,
                HasCreatedManually = false,
                TeamCode = teamCode
            };

            team.ThrowIfInvalid();

            return team;
        }
        #endregion

    }
}
