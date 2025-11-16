using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.MatchManagement.Application.Providers
{
    public interface IMatchProvider
    {
        Task<IEnumerable<UpcomingMatchInfo>> GetUpcomingMatchs();
    }
}
