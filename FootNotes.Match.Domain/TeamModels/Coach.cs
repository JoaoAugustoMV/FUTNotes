using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.MatchManagement.Domain.TeamModels
{
    public class Coach : Professional
    {
        public override bool IsValid(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
