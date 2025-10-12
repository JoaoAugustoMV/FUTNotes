using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.MatchManagement.Domain.TeamModels
{
    public class Player : Professional
    {
        public PlayerPosition Position { get; private set; }
        public override void ThrowIfInvalid()
        {
            throw new NotImplementedException();
        }
    }

    public enum PlayerPosition
    {
        Goalkeeper,
        Defender,
        Midfielder,
        Forward
    }
}
