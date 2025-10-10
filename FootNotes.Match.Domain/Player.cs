using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Match.Domain
{
    public class Player : Professional
    {
        public PlayerPosition Position { get; private set; }
        public override bool IsValid(out string msg)
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
